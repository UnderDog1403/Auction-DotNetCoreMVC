using MyApp.Models;
using MyApp.Data;
namespace MyApp.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Payment> GetAll()
        {
            return _context.Payments.ToList();
        }

        public Payment GetById(int id)
        {
            return _context.Payments.Find(id);
        }
    
        public void Add(Payment payment)
        {
            _context.Payments.Add(payment);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}