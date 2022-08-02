using Microsoft.EntityFrameworkCore;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Inputs;
using Oneonones.Domain.Outputs;
using Oneonones.Repositories.Context;
using Oneonones.Services.Contracts;
using Oneonones.Services.Exceptions;

namespace Oneonones.Services;

public class EmployeeService : IEmployeeService
{
    private readonly OneononeContext dbContext;
    private readonly DbSet<Employee> employeeDbSet;

    public EmployeeService(OneononeContext context)
    {
        dbContext = context;
        employeeDbSet = context.Employee;
    }

    public async Task<IEnumerable<EmployeeOutput>> ObtainAllAsync()
    {
        var employees = await employeeDbSet.ToListAsync();
        return employees.Select(employee => (EmployeeOutput)employee);
    }

    public async Task<EmployeeOutput> ObtainByEmailAsync(string employeeEmail)
    {
        var employee = await employeeDbSet.SingleOrDefaultAsync(employee => employee.Email == employeeEmail);
        return employee is null
            ? throw new NotFoundException()
            : (EmployeeOutput)employee;
    }

    public async Task<EmployeeOutput> ObtainByIdAsync(Guid employeeId)
    {
        var employee = await employeeDbSet.SingleOrDefaultAsync(employee => employee.Id == employeeId);
        return employee is null
            ? throw new NotFoundException()
            : (EmployeeOutput)employee;
    }

    public async Task<Guid> InsertAsync(EmployeeInput employeeInput)
    {
        var employee = new Employee
        {
            Email = employeeInput.Email!,
            Name = employeeInput.Name!,
        };

        await employeeDbSet.AddAsync(employee);
        await dbContext.SaveChangesAsync();
        return employee.Id;
    }

    public async Task<EmployeeOutput> UpdateAsync(Guid employeeId, EmployeeInput employeeInput)
    {
        var employee = await employeeDbSet.SingleOrDefaultAsync(employee => employee.Id == employeeId);
        if (employee is null) throw new NotFoundException();

        employee.Email = employeeInput.Email!;
        employee.Name = employeeInput.Name!;

        employeeDbSet.Update(employee);
        await dbContext.SaveChangesAsync();
        return (EmployeeOutput)employee;
    }

    public async Task DeleteAsync(Guid employeeId)
    {
        var employee = await employeeDbSet.SingleOrDefaultAsync(employee => employee.Id == employeeId);
        if (employee is null) throw new NotFoundException();

        employeeDbSet.Remove(employee);
        await dbContext.SaveChangesAsync();
    }
}
