using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_RNS.Data;
using Project_RNS.Models;

namespace Project_RNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Add expense

        [HttpPost("AddExpense")]
        public async Task<IActionResult> AddExpense([FromBody] ExpenseDto expenseDto)
        {

            var addExp = new Expense();

            addExp.Amount = expenseDto.Amount;
            addExp.Id = expenseDto.Id;
            addExp.Category = expenseDto.Category;
            addExp.Date = expenseDto.Date;
            addExp.Description = expenseDto.Description;

            if (expenseDto.Base64data != null)
            {
                byte[] img = Convert.FromBase64String(expenseDto.Base64data);

                addExp.BillDoc = img;
            }

            _context.Expenses.Add(addExp);
            await _context.SaveChangesAsync();
            return Ok(expenseDto);
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {

            var reg = new Login();

            reg.Id = signUpDto.Id;
            reg.Name = signUpDto.Name;
            reg.Email = signUpDto.Email;
            reg.PhoneNumber = signUpDto.PhoneNumber;
            reg.Password = signUpDto.Password;

            _context.SignUps.Add(reg);
            await _context.SaveChangesAsync();
            return Ok(signUpDto);
        }

        #endregion



        #region get expense
        // Get All Expenses
        [HttpGet("ViewExpense")]
        public async Task<IActionResult> GetExpenses()
        {
            var expenses = await _context.Expenses.ToListAsync();
            return Ok(expenses);
        }

        [HttpGet("EditExpence/{Id}")]

        public async Task<IActionResult> GetEditExpense(int Id)
        {
            var expenses = await _context.Expenses.FindAsync(Id);
            return Ok(expenses);
        }

        // Get All User
        [HttpGet("User")]
        public async Task<IActionResult> GetUser()
        {
            var users = await _context.SignUps.ToListAsync();
            return Ok(users);
        }

        [HttpGet("User/{Id}")]

        public async Task<IActionResult> GetEditUser(int Id)
        {
            var users = await _context.SignUps.FindAsync(Id);
            return Ok(users);
        }


        [HttpGet("LoginPage/{Email}/{Password}")]

        public async Task<IActionResult> GetLoginUser(String Email, String Password)
        {
            var valid = _context.SignUps.Any(x => x.Email == Email && x.Password == Password);

            if (valid)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }




        #endregion


        #region Update Data

        // Update Expense
        [HttpPut("UpdateExpense/{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseDto updatedExpense)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null) return NotFound();

            expense.Category = updatedExpense.Category;
            expense.Description = updatedExpense.Description;
            expense.Amount = updatedExpense.Amount;
            expense.Date = updatedExpense.Date;

            if (updatedExpense.Base64data != null)
            {
                byte[] img = Convert.FromBase64String(updatedExpense.Base64data);

                expense.BillDoc = img;
            }
            await _context.SaveChangesAsync();

            return Ok(expense);
        }

        #endregion


        #region Delete

        // Delete Expense
        [HttpDelete("DeleteExpense{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null) return NotFound();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return Ok();
        }

        #endregion


        #region filter

        // Filter Expenses by Date Range and Category
        [HttpGet("filter")]
        public async Task<IActionResult> FilterExpenses(DateTime? startDate, DateTime? endDate
            //, string category
            )
        {
            var expenses = _context.Expenses.AsQueryable();

            if (startDate.HasValue)
                expenses = expenses.Where(e => e.Date >= startDate.Value);

            if (endDate.HasValue)
                expenses = expenses.Where(e => e.Date <= endDate.Value);

            //if (!string.IsNullOrEmpty(category))
            //    expenses = expenses.Where(e => e.Category == category);

            return Ok(await expenses.ToListAsync());

        }

        #endregion


    }
}
