using Microsoft.EntityFrameworkCore;
using Oneonones.Domain.Entities;
using Oneonones.Domain.Inputs;
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

    public async Task<IEnumerable<Employee>> ObtainAllAsync()
    {
        var employees = await employeeDbSet.ToListAsync();
        return employees;
    }

    public async Task<Employee> ObtainByEmailAsync(string employeeEmail)
    {
        var employee = await employeeDbSet.SingleOrDefaultAsync(employee => employee.Email == employeeEmail);
        return employee ?? throw new NotFoundException("Not found");
    }

    public async Task<Employee> ObtainByIdAsync(Guid employeeId)
    {
        var employee = await employeeDbSet.SingleOrDefaultAsync(employee => employee.Id == employeeId);
        return employee ?? throw new NotFoundException("Not found");
    }

    public async Task<Guid> InsertAsync(EmployeeInput employeeInput)
    {
        var employee = new Employee(employeeInput.Email!, employeeInput.Name!);
        await employeeDbSet.AddAsync(employee);
        await dbContext.SaveChangesAsync();
        return employee.Id;
    }

    public async Task<Employee> UpdateAsync(Guid employeeId, EmployeeInput employeeInput)
    {
        var employee = await ObtainByIdAsync(employeeId);
        employee.Update(employeeInput.Email!, employeeInput.Name!);
        employeeDbSet.Update(employee);
        await dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task DeleteAsync(Guid employeeId)
    {
        var employee = await ObtainByIdAsync(employeeId);
        employeeDbSet.Remove(employee);
        await dbContext.SaveChangesAsync();
    }
}
