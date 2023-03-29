using System;

class PhoneBook
{
    private class PhoneEntry
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public PhoneEntry Next { get; set; }
    }

    private const int TableSize = 100;
    private PhoneEntry[] phoneTable = new PhoneEntry[TableSize];

    private int GetHash(string key)
    {
        int hash = 0;
        foreach (char c in key)
        {
            hash = (hash << 5) + hash + c;
        }
        return Math.Abs(hash) % TableSize;
    }

    private PhoneEntry FindPhoneEntry(string name)
    {
        int hash = GetHash(name);
        PhoneEntry entry = phoneTable[hash];
        while (entry != null && entry.Name != name)
        {
            entry = entry.Next;
        }
        return entry;
    }

    public string GetPhoneNumber(string name)
    {
        PhoneEntry entry = FindPhoneEntry(name);
        if (entry != null)
        {
            return entry.PhoneNumber;
        }
        else
        {
            return null;
        }
    }

    public void AddPhoneNumber(string name, string phoneNumber)
    {
        int hash = GetHash(name);
        PhoneEntry entry = phoneTable[hash];
        while (entry != null && entry.Name != name)
        {
            entry = entry.Next;
        }
        if (entry == null)
        {
            entry = new PhoneEntry { Name = name, PhoneNumber = phoneNumber, Next = phoneTable[hash] };
            phoneTable[hash] = entry;
        }
        else
        {
            entry.PhoneNumber = phoneNumber;
        }
    }

    static void Main()
    {
        PhoneBook phoneBook = new PhoneBook();

        // Agregar números de teléfono al diccionario
        phoneBook.AddPhoneNumber("Juan Perez", "555-1234");
        phoneBook.AddPhoneNumber("Maria Gomez", "555-5678");
        phoneBook.AddPhoneNumber("Pedro Martinez", "555-9012");
        phoneBook.AddPhoneNumber("Luisa Gonzalez", "555-3456");
        phoneBook.AddPhoneNumber("Ana Ramirez", "555-7890");

        // Buscar número de teléfono por nombre
        Console.WriteLine("Ingrese el nombre de la persona para buscar su número de teléfono: ");
        string name = Console.ReadLine();
        string phoneNumber = phoneBook.GetPhoneNumber(name);
        if (phoneNumber != null)
        {
            Console.WriteLine("El número de teléfono de {0} es {1}", name, phoneNumber);
        }
        else
        {
            Console.WriteLine("El nombre ingresado no se encuentra en la lista de contactos.");
        }

        // Agregar un nuevo número de teléfono
        Console.WriteLine("Ingrese el nombre de la persona a la que desea agregar un número de teléfono: ");
        string newName = Console.ReadLine();
        Console.WriteLine("Ingrese el número de teléfono de {0}: ", newName);
        string newPhone = Console.ReadLine();
        phoneBook.AddPhoneNumber(newName, newPhone);
        Console.WriteLine("El número de teléfono de {0} ha sido agregado a la lista de contactos.", newName);

        Console.ReadLine();
    }
}