using Game_AiLaTrieuPhu.A_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_AiLaTrieuPhu.B_BUS
{
    internal class GameServices
    {
        Repositories repos;
        public GameServices()
        {
            repos = new Repositories();
        }
        // 1. Hàm random câu hỏi để load vào form 
        public Question RandomQuesetion(int level)
        {
            //Bước 1: Lấy ra danh sách câu hỏi 
            var listQuestion = repos.GetAllQuestion();

            // Bước 2: Lấy ra nhưng câu hỏi trong level đó
            var questionlv = listQuestion.Where(x => x.Level == level).ToList();

            //Ramdom ra 1 câu hỏi lv đó
            Random r = new Random();
            int index = r.Next(questionlv.Count);

            return questionlv[index];
        }
        public int CountNumberLevel(int level)//trả về số lượng câu hỏi đấy
        {
            return repos.GetAllQuestion().Where(x => x.Level == level).Count();
        }

        // 2. Check xem câu hỏi đã đúng hay chưa.
        public bool CheckTrueAnswer(int questionID, string answer)
        {
            var question = repos.GetAllQuestion().FirstOrDefault(x => x.Id == questionID);
            return question.TrueAnswer == answer;
        }

        public string GetTrueAnswer(int questionID)
        {
            return repos.GetAllQuestion().FirstOrDefault(p => p.Id == questionID).TrueAnswer;
        }

    }
}
