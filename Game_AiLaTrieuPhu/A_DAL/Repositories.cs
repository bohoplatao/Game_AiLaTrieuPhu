using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_AiLaTrieuPhu.A_DAL
{
    internal class Repositories
    {
        // rep chỉ để load câu hỏi ra thôi 
        GameDbContext _context;
        public Repositories()
        {
            _context = new GameDbContext();
        }
        public List<Question> GetAllQuestion()
        {
            return _context.Questions.ToList();
        }
    }
}
