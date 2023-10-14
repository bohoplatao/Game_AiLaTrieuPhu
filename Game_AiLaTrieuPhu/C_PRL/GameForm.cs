using Game_AiLaTrieuPhu.A_DAL;
using Game_AiLaTrieuPhu.B_BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Game_AiLaTrieuPhu.C_PRL
{
    public partial class GameForm : Form
    {
        GameServices _service = new GameServices();
        List<int> selectQuestion = new List<int>();
        int chooseID;//id của câu hỏi đang đc chọn tại thời điểm chơi
        int[] questionMoney = new int[15] {1000000, 2000000, 5000000, 7000000, 10000000, 15000000, 20000000, 50000000, 75000000, 100000000, 150000000, 200000000, 400000000, 700000000, 1000000000 };
        int selectIndex =0;
        int timelimit = 30;//gán 1 biến toàn cục để làm tg đếm ngược 
        int level = 1;
        List<Button> buttons = new List<Button>();
        SoundPlayer musicnen = new SoundPlayer(@"C:\Users\admin\OneDrive\Máy tính\Anh_ALTP\Assets\1to5.wav");
        SoundPlayer musictrue = new SoundPlayer(@"C:\Users\admin\OneDrive\Máy tính\Anh_ALTP\Assets\correct.wav");
        SoundPlayer musicfalse = new SoundPlayer(@"C:\Users\admin\OneDrive\Máy tính\Anh_ALTP\Assets\sai.wav");
        SoundPlayer musicwin = new SoundPlayer(@"C:\Users\admin\OneDrive\Máy tính\Anh_ALTP\Assets\win.wav");
        public GameForm()
        {
            InitializeComponent();
            foreach (Button item in grb_Moc.Controls)
            {
                if(item is Button && !item.Name.Contains("btn_Play"))
                {
                    buttons.Add(item);
                }
            }
            musicnen.Play();
            

        }

        private void btn_Play_Click(object sender, EventArgs e)
        {
            btn_1.BackColor = Color.BlueViolet;
            btn_Play.Enabled = false;
            //if (selectIndex <= 5)
            //{
            //    ptb_Change.Visible = false;
            //}
            //else if (selectIndex > 5)
            //{

            //    ptb_Change.Enabled = true;
            //}
            RanDomQuestonShow();

            time_Limit.Start();
            
            //MessageBox.Show(string.Join("-", selectQuestion));



        }
        public void RanDomQuestonShow()
        {
            lb_time.Text = "30";
            timelimit = 30;
            
            
            while (selectQuestion.Count < _service.CountNumberLevel(level))
            {
                Question question = _service.RandomQuesetion(level);
                if (!selectQuestion.Contains(question.Id))
                {
                    txt_Question.Text =$"Câu hỏi số{(selectIndex+1)}: " + question.QuestionText;
                    btn_A.Text = question.AnswerA;
                    btn_B.Text = question.AnswerB;
                    btn_C.Text = question.AnswerC;
                    btn_D.Text = question.AnsewerD;
                    selectQuestion.Add(question.Id);
                    chooseID = question.Id;
                    
                    break;
                }
                else continue;
            }
        }

        public void CheckTrue(string answer)
        {
            if (_service.CheckTrueAnswer(chooseID, answer))

            {
                musictrue.Play();
                MessageBox.Show("Đúng");
                lb_money.Text = questionMoney[selectIndex].ToString();
                selectIndex++;
                // nếu vị trí câu hỏi nó từ 6-10 thì câu hỏi level2, 11-15 level3
                if (selectIndex == 10) { level = 3; selectQuestion.Clear(); }
                else if (selectIndex == 5) { level = 2; selectQuestion.Clear(); }
                foreach (Button item in buttons)
                {
                    int indexbt = Convert.ToInt32(item.Name.Replace("btn_", ""));
                    if(indexbt <= selectIndex)
                    {
                        item.BackColor = Color.Green;
                    }
                }

          
                //sau khi update level thì randomquestion sẽ theo lv đó
                RanDomQuestonShow();
                if(selectIndex == 15)
                {
                    musicwin.Play();
                    MessageBox.Show("Ban da chien thang tro choi <3");
                    return;
                }
            }
            else

            {
                //if(selectQuestion.Count >= 5)
                //{
                //    lb_money.Text = resetmoney1.ToString();
                //    MessageBox.Show("Sai.bye. Bạn ra về với số tiền là " + resetmoney1 );
                //}
                //else if (selectQuestion.Count >= 10)
                //{
                //    lb_money.Text = resetmoney1.ToString();
                //    MessageBox.Show("Sai.bye. Bạn ra về với số tiền là " + resetmoney2);
                //}
                musicfalse.Play();
                MessageBox.Show("Sai.bye. Bạn ra về với số tiền là " + lb_money.Text);
                grb_CauHoi.Enabled = false;
                return;
            }
        }
        private void btn_A_Click(object sender, EventArgs e)
        {

            CheckTrue("A");
           
            
        }

        private void btn_B_Click(object sender, EventArgs e)
        {
            CheckTrue("B");
            
            
        }

        private void btn_C_Click(object sender, EventArgs e)
        {
            CheckTrue("C");
            
            
        }

        private void btn_D_Click(object sender, EventArgs e)
        {
            CheckTrue("D");
            
            
        }

        private void ptb_5050_Click(object sender, EventArgs e)
        {
            string trueAnswer = _service.GetTrueAnswer(chooseID);
            Random r = new Random();
            int hold = r.Next(3);
            if(trueAnswer == "A")
            {
                 if(hold == 1) { btn_C.Text = "";btn_D.Text = ""; }
                else if (hold == 2) { btn_B.Text = ""; btn_D.Text = ""; }
                else { btn_C.Text = ""; btn_B.Text = ""; };
            }else if(trueAnswer == "B")
            {
                 if(hold == 1) { btn_C.Text = ""; btn_D.Text = ""; }
                else if(hold == 2) { btn_A.Text = ""; btn_D.Text = ""; }
                else { btn_C.Text = ""; btn_A.Text = ""; };
            }else if(trueAnswer == "C")
            {
                if (hold == 1) { btn_B.Text = ""; btn_D.Text = ""; }
                else if (hold == 2) { btn_A.Text = ""; btn_D.Text = ""; }
                else { btn_A.Text = ""; btn_B.Text = ""; };
            }
            else
            {
                if (hold == 1) { btn_C.Text = ""; btn_B.Text = ""; }
                else if (hold == 2) { btn_A.Text = ""; btn_C.Text = ""; }
                else { btn_A.Text = ""; btn_B.Text = ""; };
            }
            // cachs 1 => Disable nnay
            //ptb_5050.Enabled = false;
            //cách 2 là ẩn nó luôn đi => thuộ tính visible là thuộc tính chỉ khả năng hiển thị của 1 control
            ptb_5050.Visible = false;
        }

        private void ptb_Viewer_Click(object sender, EventArgs e)
        {

            string trueAnswer = _service.GetTrueAnswer(chooseID);
            Random r = new Random();
            int r1 = r.Next(0,30);
            int r2 = r.Next(0, 30);
            int r3 = r.Next(0, 30);
            int rTrue = (100 - r1 - r2 - r3);
            string show;
            if (trueAnswer == "A") show = $"A: {rTrue} \nB {r1} \n C: {r2}\nD:{r3}";
            else if (trueAnswer == "B") show = $"A: {r1} \nB {rTrue} \n C: {r2}\nD:{r3}";
            else if (trueAnswer == "C") show = $"A: {r1} \nB {r2} \n C: {rTrue}\nD:{r3}";
            else show = $"A: {r1} \nB {r2} \n C: {r3}\nD:{r}";
            MessageBox.Show(show);
            ptb_Viewer.Visible = false;
        }

        private void ptb_BacHoc_Click(object sender, EventArgs e)
        {
            string trueAnswer = _service.GetTrueAnswer(chooseID);
            MessageBox.Show("Chuyên Gia Khuyên Bạn CHọn đáp án : " + trueAnswer);
            ptb_BacHoc.Visible = true;
        }

        private void ptb_Change_Click(object sender, EventArgs e)
        {
            //khi skip thif cái gì thay đổi 
            //load ra câu hỏi mới
            //RanDomQuestonShow();
            //TH1 cho ko 1 câu hỏi
            //lb_money.Text = questionMoney[selectIndex].ToString();
            //// thực hiện update selectedIndex (vị trí câu hỏi trong 15 câu)
            //selectIndex++;

            //chỉ thay đổi câu hỏi thôi
            RanDomQuestonShow();
            ptb_Change.Visible = false;
        }

        private void time_Limit_Tick(object sender, EventArgs e)
        {
            //khi nào timer chạy: Khi bắt đầu trò chơi hoặc có câu hỏi mới
            // 1.time_limit giảm dần từng giây
            timelimit--;
            time_Limit.Interval = 1000;
            // 2. Gán giá trị của timer cho label hiển thị thời gian đếm ngược 
            lb_time.Text = timelimit.ToString();
            // nếu hết giờ thì báo thua cuộc
            if(lb_time.Text == "0")
            {
                time_Limit.Stop();
                if (selectQuestion.Count >= 5)
                {
                    lb_money.Text = "10000000";
                }
                else if (selectQuestion.Count >= 10)
                {
                    lb_money.Text = "100000000";
                }
                MessageBox.Show($"Hết thời gian suy nghĩ, bạn ra về với {lb_money.Text} đồng");
                
            }
        }
    }
}
