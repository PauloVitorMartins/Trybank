namespace trybank;

public class Trybank
{
    public bool Logged;
    public int loggedUser;

    //0 -> Número da conta
    //1 -> Agência hihi
    //2 -> Senha
    //3 -> Saldo
    public int[,] Bank;
    public int registeredAccounts;
    private int maxAccounts = 50;
    public Trybank()
    {
        loggedUser = -99;
        registeredAccounts = 0;
        Logged = false;
        Bank = new int[maxAccounts, 4];
    }

    // 1. Construa a funcionalidade de cadastrar novas contas
    public void RegisterAccount(int number, int agency, int pass)
    {
        // itera pela matriz Bank e verifica se existe essa combinação de agência e número de conta.
        for (int i = 0; i < registeredAccounts; i++)
        {
            if (Bank[i, 0] == number && Bank[i, 1] == agency)
            {
                // se existir essa combinação lança o novo erro
                throw new ArgumentException("A conta já está sendo usada!");
            }
        }
        Bank[registeredAccounts, 0] = number;
        Bank[registeredAccounts, 1] = agency;
        Bank[registeredAccounts, 2] = pass;
        Bank[registeredAccounts, 3] = 0;
        registeredAccounts += 1;
    }

    // 2. Construa a funcionalidade de fazer Login
    public void Login(int number, int agency, int pass)
    {
        if (Logged)
        {
            throw new AccessViolationException("Usuário já está logado");
        }
        for (int i = 0; i < registeredAccounts; i++)
        {
            if (Bank[i, 0] == number && Bank[i, 1] == agency && Bank[i, 2] == pass)
            {
                Logged = true;
                loggedUser = i;
            }
            else if (Bank[i, 0] == number && Bank[i, 1] == agency && Bank[i, 2] != pass)
            {
                throw new ArgumentException("Senha incorreta");
            }
            else
            {
                throw new ArgumentException("Agência + Conta não encontrada");
            }
        }
    }

    // 3. Construa a funcionalidade de fazer Logout
    public void Logout()
    {
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        else
        {
            Logged = false;
            loggedUser = -99;
        }
    }

    // 4. Construa a funcionalidade de checar o saldo
    public int CheckBalance()
    {
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        else
        {
            return Bank[loggedUser, 3];
        }
    }

    // 5. Construa a funcionalidade de depositar dinheiro
    public void Deposit(int value)
    {
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        Bank[loggedUser, 3] = Bank[loggedUser, 3] + value;
    }

    // 6. Construa a funcionalidade de sacar dinheiro
    public void Withdraw(int value)
    {
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        else if (Bank[loggedUser, 3] >= value)
        {
            Bank[loggedUser, 3] = Bank[loggedUser, 3] - value;
        }
        else
        {
            throw new InvalidOperationException("Saldo insuficiente");
        }
    }

    // 7. Construa a funcionalidade de transferir dinheiro entre contas
    public void Transfer(int destinationNumber, int destinationAgency, int value)
    {
        bool destinyExist = false;
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        for (int h = 0; h < registeredAccounts; h++)
        {
            if (Bank[h, 0] == destinationNumber && Bank[h, 1] == destinationAgency)
            {
                destinyExist = true;
            }
        }
        if (Bank[loggedUser, 3] < value)
        {
            throw new InvalidOperationException("Saldo insuficiente");
        }
        else if (destinyExist)
        {
            Bank[loggedUser, 3] = Bank[loggedUser, 3] - value;
            for (int e = 0; e < registeredAccounts; e++)
            {
                if (Bank[e, 0] == destinationNumber && Bank[e, 1] == destinationAgency)
                {
                    Bank[e, 3] = Bank[e, 3] + value;
                }
            }
        }
        else
        {
            Console.WriteLine("Conta destino não existe");
        }
    }


}
