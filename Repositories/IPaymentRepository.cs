using MyApp.Models;
using System.Collections.Generic;

namespace MyApp.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment GetById(int id);
 
        void Add(Payment payment);
        void Save();
    }
}
