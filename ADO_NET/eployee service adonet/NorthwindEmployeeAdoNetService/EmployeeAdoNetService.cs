using System.Runtime.CompilerServices;

namespace NorthwindEmployeeAdoNetService;

/// <summary>
/// A service for interacting with the "Employees" table using ADO.NET.
/// </summary>
public sealed class EmployeeAdoNetService
{
    private readonly DbProviderFactory _providerFactory;
    private readonly string _connectionString;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeAdoNetService"/> class.
    /// </summary>
    /// <param name="dbFactory">The database provider factory used to create database connection and command instances.</param>
    /// <param name="connectionString">The connection string used to establish a database connection.</param>
    /// <exception cref="ArgumentNullException">Thrown when either <paramref name="dbFactory"/> or <paramref name="connectionString"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is empty or contains only white-space characters.</exception>
    public EmployeeAdoNetService(DbProviderFactory dbFactory, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(dbFactory);
        ArgumentNullException.ThrowIfNull(connectionString);
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        _providerFactory = dbFactory;
        _connectionString = connectionString;
    }

    /// <summary>
    /// Retrieves a list of all employees from the Employees table of the database.
    /// </summary>
    /// <returns>A list of Employee objects representing the retrieved employees.</returns>
    public IList<Employee> GetEmployees()
    {
        using var connection = _providerFactory.CreateConnection();
        connection.ConnectionString = _connectionString;

        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Employees";

        List<Employee> employees = new List<Employee>();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var id = reader.GetInt64(reader.GetOrdinal("EmployeeID"));

            var employee = new Employee(id)
            {
                FirstName = reader["FirstName"]?.ToString(),
                LastName = reader["LastName"]?.ToString(),
                Title = reader["Title"] as string,
                TitleOfCourtesy = reader["TitleOfCourtesy"] as string,
                BirthDate = reader["BirthDate"] as DateTime?,
                HireDate = reader["HireDate"] as DateTime?,
                Address = reader["Address"] as string,
                City = reader["City"] as string,
                Region = reader["Region"] as string,
                PostalCode = reader["PostalCode"] as string,
                Country = reader["Country"] as string,
                HomePhone = reader["HomePhone"] as string,
                Extension = reader["Extension"] as string,
                Notes = reader["Notes"] as string,
                ReportsTo = reader["ReportsTo"] as long?,
                PhotoPath = reader["PhotoPath"] as string
            };

            employees.Add(employee);
        }

        return employees;
    }

    /// <summary>
    /// Retrieves an employee with the specified employee ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to retrieve.</param>
    /// <returns>The retrieved an <see cref="Employee"/> instance.</returns>
    /// <exception cref="EmployeeServiceException">Thrown if the employee is not found.</exception>
    public Employee GetEmployee(long employeeId)
    {
        using var connection = _providerFactory.CreateConnection();
        connection.ConnectionString = _connectionString;

        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Employees WHERE EmployeeID = @employeeId";

        var param = command.CreateParameter();
        param.ParameterName = "@employeeId";
        param.Value = employeeId;

        command.Parameters.Add(param);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            var id = reader.GetInt64(reader.GetOrdinal("EmployeeID"));

            var employee = new Employee(id)
            {
                FirstName = reader["FirstName"]?.ToString(),
                LastName = reader["LastName"]?.ToString(),
                Title = reader["Title"] as string,
                TitleOfCourtesy = reader["TitleOfCourtesy"] as string,
                BirthDate = reader["BirthDate"] as DateTime?,
                HireDate = reader["HireDate"] as DateTime?,
                Address = reader["Address"] as string,
                City = reader["City"] as string,
                Region = reader["Region"] as string,
                PostalCode = reader["PostalCode"] as string,
                Country = reader["Country"] as string,
                HomePhone = reader["HomePhone"] as string,
                Extension = reader["Extension"] as string,
                Notes = reader["Notes"] as string,
                ReportsTo = reader["ReportsTo"] as long?,
                PhotoPath = reader["PhotoPath"] as string
            };

            return employee;
        }

        throw new EmployeeServiceException();
    }

    /// <summary>
    /// Adds a new employee to Employee table of the database.
    /// </summary>
    /// <param name="employee">The  <see cref="Employee"/> object containing the employee's information.</param>
    /// <returns>The ID of the newly added employee.</returns>
    /// <exception cref="EmployeeServiceException">Thrown when an error occurs while adding the employee.</exception>
    public long AddEmployee(Employee employee)
    {
        using var connection = _providerFactory.CreateConnection();
        connection.ConnectionString = _connectionString;

        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
        INSERT INTO Employees 
        (FirstName, LastName, Title, TitleOfCourtesy, BirthDate, HireDate,
         Address, City, Region, PostalCode, Country, HomePhone,
         Extension, Notes, ReportsTo, PhotoPath)
        VALUES 
        (@FirstName, @LastName, @Title, @TitleOfCourtesy, @BirthDate, @HireDate,
         @Address, @City, @Region, @PostalCode, @Country, @HomePhone,
         @Extension, @Notes, @ReportsTo, @PhotoPath);

        SELECT last_insert_rowid();";

        AddParameter(command, "@FirstName", employee.FirstName);
        AddParameter(command, "@LastName", employee.LastName);
        AddParameter(command, "@Title", employee.Title);
        AddParameter(command, "@TitleOfCourtesy", employee.TitleOfCourtesy);
        AddParameter(command, "@BirthDate", employee.BirthDate);
        AddParameter(command, "@HireDate", employee.HireDate);
        AddParameter(command, "@Address", employee.Address);
        AddParameter(command, "@City", employee.City);
        AddParameter(command, "@Region", employee.Region);
        AddParameter(command, "@PostalCode", employee.PostalCode);
        AddParameter(command, "@Country", employee.Country);
        AddParameter(command, "@HomePhone", employee.HomePhone);
        AddParameter(command, "@Extension", employee.Extension);
        AddParameter(command, "@Notes", employee.Notes);
        AddParameter(command, "@ReportsTo", employee.ReportsTo);
        AddParameter(command, "@PhotoPath", employee.PhotoPath);

        var result = command.ExecuteScalar();

        if (result == null)
            throw new EmployeeServiceException("Inserting an employee failed.");

        return Convert.ToInt64(result);
    }

    /// <summary>
    /// Removes an employee from the the Employee table of the database based on the provided employee ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to remove.</param>
    /// <exception cref="EmployeeServiceException"> Thrown when an error occurs while attempting to remove the employee.</exception>
    public void RemoveEmployee(long employeeId)
    {
        try
        {
            using var connection = _providerFactory.CreateConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Employees WHERE EmployeeID = @id";

            AddParameter(command, "@id", employeeId);

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new EmployeeServiceException("Error removing employee.", ex);
        }
    }

    /// <summary>
    /// Updates an employee record in the Employee table of the database.
    /// </summary>
    /// <param name="employee">The employee object containing updated information.</param>
    /// <exception cref="EmployeeServiceException">Thrown when there is an issue updating the employee record.</exception>
    public void UpdateEmployee(Employee employee)
    {
        using var connection = _providerFactory.CreateConnection();
        connection.ConnectionString = _connectionString;

        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = @"
        UPDATE Employees
        SET FirstName = @FirstName,
            LastName = @LastName,
            Title = @Title,
            TitleOfCourtesy = @TitleOfCourtesy,
            BirthDate = @BirthDate,
            HireDate = @HireDate,
            Address = @Address,
            City = @City,
            Region = @Region,
            PostalCode = @PostalCode,
            Country = @Country,
            HomePhone = @HomePhone,
            Extension = @Extension,
            Notes = @Notes,
            ReportsTo = @ReportsTo,
            PhotoPath = @PhotoPath
        WHERE EmployeeID = @id;
        ";

        AddParameter(command, "@FirstName", employee.FirstName);
        AddParameter(command, "@LastName", employee.LastName);
        AddParameter(command, "@Title", employee.Title);
        AddParameter(command, "@TitleOfCourtesy", employee.TitleOfCourtesy);
        AddParameter(command, "@BirthDate", employee.BirthDate);
        AddParameter(command, "@HireDate", employee.HireDate);
        AddParameter(command, "@Address", employee.Address);
        AddParameter(command, "@City", employee.City);
        AddParameter(command, "@Region", employee.Region);
        AddParameter(command, "@PostalCode", employee.PostalCode);
        AddParameter(command, "@Country", employee.Country);
        AddParameter(command, "@HomePhone", employee.HomePhone);
        AddParameter(command, "@Extension", employee.Extension);
        AddParameter(command, "@Notes", employee.Notes);
        AddParameter(command, "@ReportsTo", employee.ReportsTo);
        AddParameter(command, "@PhotoPath", employee.PhotoPath);
        AddParameter(command, "@id", employee.Id);

        var result = command.ExecuteNonQuery();

        if (result == 0)
        {
            throw new EmployeeServiceException();
        }
    }

    private static void AddParameter(DbCommand command, string name, object? value)
    {
        var param = command.CreateParameter();
        param.ParameterName = name;
        param.Value = value ?? DBNull.Value;
        command.Parameters.Add(param);
    }
}
