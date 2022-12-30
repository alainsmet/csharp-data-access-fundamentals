using Microsoft.Data.SqlClient;

var connectionString =
    "Data Source=(LocalDB)\\MSSQLLocalDB;" +
    "Initial Catalog=WarehouseManagement;" +
    "Integrated Security=True;";

using SqlConnection connection = new SqlConnection(connectionString);

using SqlCommand command = new SqlCommand (
    "SELECT * FROM [Orders] INNER JOIN [Customers] ON [Customers].Id = [Orders].CustomerId",
    connection
);

connection.Open();

using var reader = command.ExecuteReader();

if (reader.HasRows == false)
{
    return;
}

while (reader.Read())
{
    var orderId = reader["Id"];
    var customer = reader["Name"];

    Console.WriteLine(orderId);
    Console.WriteLine(customer);
}

reader.Close();

Console.WriteLine("Press ENTER to continue ...");
Console.ReadLine();

using SqlCommand command2 = new SqlCommand(
    @"INSERT INTO [Customers] (Id, Name, Address, PostalCode, Country, PhoneNumber) VALUES(NEWID(), @Name, @Address, @PostalCode, @Country, @PhoneNumber)",
    connection
);

var nameParameter = new SqlParameter("Name", System.Data.SqlDbType.NVarChar);
Console.Write("Name : ");
nameParameter.Value = Console.ReadLine().Trim();
command2.Parameters.Add(nameParameter);

var addressParameter = new SqlParameter("Address", System.Data.SqlDbType.NVarChar);
Console.Write("Address : ");
addressParameter.Value = Console.ReadLine().Trim();
command2.Parameters.Add(addressParameter);

var postalCodeParameter = new SqlParameter("PostalCode", System.Data.SqlDbType.NVarChar);
Console.Write("Postal code : ");
postalCodeParameter.Value = Console.ReadLine().Trim();
command2.Parameters.Add(postalCodeParameter);

var countryParameter = new SqlParameter("Country", System.Data.SqlDbType.NVarChar);
Console.Write("Country : ");
countryParameter.Value = Console.ReadLine().Trim();
command2.Parameters.Add(countryParameter);

var phoneNumberParameter = new SqlParameter("PhoneNumber", System.Data.SqlDbType.NVarChar);
Console.Write("Phone number : ");
phoneNumberParameter.Value = Console.ReadLine().Trim();
command2.Parameters.Add(phoneNumberParameter);

//connection.Open();

var rowsAffected = command2.ExecuteNonQuery();

Console.WriteLine($"Rows affected : {rowsAffected}");
Console.WriteLine();
Console.WriteLine("Press ENTER to continue ...");
Console.ReadLine();
