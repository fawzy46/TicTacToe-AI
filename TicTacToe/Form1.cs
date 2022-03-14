using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        Bitmap unSeen;
        int w,h;
        int currplayer = 1;
        bool gameover = false;
        CActor[,] board = new CActor[3,3];
        List<CActor> L = new List<CActor>();    
        List<CActor> L2 = new List<CActor>();       
        List<CActor> L3 = new List<CActor>();
        public Form1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.BackColor = Color.White;
            w = this.ClientSize.Width;
            h = this.ClientSize.Height;
        }
        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int i = e.X / (w / 3);
            int j = e.Y / (h / 3);
            char temp;
            if(board[i,j] == null /*&& !gameover*/)
            {
                CActor pnn = new CActor();
                if (currplayer == 1)
                {
                    pnn.im = new Bitmap("X.png");
                    pnn.c = 'X';
                }
                pnn.x = (w / 3) * i + 10;
                pnn.y = (h / 3) * j + 10;
                board[i, j] = pnn;
                temp = checkwin();
                if (temp == 'X')
                {
                    DrawDubb(this.CreateGraphics());
                    MessageBox.Show("X Won!!!!");
                }
                else
                {
                    best_move();
                }
                DrawDubb(this.CreateGraphics());
            }
        }
        void best_move()
        {
            CActor[,] b = new CActor[3, 3];
            int b_score = -999;
            int x = -1, y = -1;
            int f = 0;
            char temp;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(board[i,j] == null)
                    {
                        CActor pnn = new CActor();
                        pnn.c = 'O';
                        pnn.x = (w / 3) * i + 10;
                        pnn.y = (h / 3) * j + 10;
                        board[i, j] = pnn;
                        int score = minmax(false);
                        board[i, j] = null;
                        if (score > b_score)
                        {
                            b_score = score;
                            x = i;
                            y = j;
                        }
                    }
                }
            }
            CActor pnn2 = new CActor();
            pnn2.im = new Bitmap("O.png");
            pnn2.c = 'O';
            pnn2.x = (w / 3) * x + 10;
            pnn2.y = (h / 3) * y + 10;
            board[x, y] = pnn2;
            DrawDubb(this.CreateGraphics());
            temp = checkwin();
            if(temp != 'j')
            {
                if(temp == 'T')
                {
                    MessageBox.Show("Tie!!!");
                }
                else
                {
                    MessageBox.Show(temp + " Won!!!");
                }
            }
        }
        int minmax(bool max)
        {
            char check = checkwin();
            if(check == 'O' || check == 'X' || check == 'T')
            {
                if(check == 'O')
                {
                    return 10;
                }
                if (check == 'X')
                {
                    return -10;
                }
                if (check == 'T')
                {
                    return 0;
                }
            }
            if (max)
            {
                int bestscore = -999;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == null)
                        {
                            CActor pnn = new CActor();
                            pnn.c = 'O';
                            pnn.x = (w / 3) * i + 10;
                            pnn.y = (h / 3) * j + 10;
                            board[i, j] = pnn;
                            int score = minmax(false);
                            board[i, j] = null;
                            bestscore = Math.Max(score, bestscore);
                        }
                    }
                }
                return bestscore;
            }
            else
            {
                int bestscore = 9999;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == null)
                        {
                            CActor pnn = new CActor();
                            pnn.c = 'X';
                            pnn.x = (w / 3) * i + 10;
                            pnn.y = (h / 3) * j + 10;
                            board[i, j] = pnn;
                            int score = minmax(true);
                            board[i, j] = null;
                            bestscore = Math.Min(score, bestscore);
                        }
                    }
                }
                return bestscore;
            }
        }
        char checkwin()
        {
            char winner = 'j';
            int empty = 0;
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 1] != null && board[i, 2] != null && board[i, 0] != null && board[i,0].c == board[i,1].c && board[i,0].c == board[i,2].c)
                {
                    gameover = true;
                    winner = board[i, 0].c;
                    break;
                }
                if (board[1, i] != null && board[2, i] != null && board[0, i] != null && board[0, i].c == board[1, i].c && board[0, i].c == board[2, i].c)
                {
                    gameover = true;
                    winner = board[0, i].c;
                    break;
                }
                if (board[1, 1] != null && board[2, 2] != null && board[0, 0] != null && board[0, 0].c == board[1, 1].c && board[0, 0].c == board[2, 2].c)
                {
                    gameover = true;
                    winner = board[1, 1].c;
                    break;
                }
                if (board[2, 0] != null && board[1, 1] != null && board[0, 2] != null && board[2, 0].c == board[1, 1].c && board[2, 0].c == board[0, 2].c)
                {
                    gameover = true;
                    winner = board[2, 0].c;
                    break;
                }
                else
                {
                    empty = 0;
                    bool f = false;
                    for(int k = 0; k <3;k++)
                    {
                        for(int w = 0; w < 3; w++)
                        {
                            if(board[w,k] !=  null)
                            {
                                empty++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if(empty == 9)
                    {
                        winner = 'T';
                        gameover = true;
                        break;
                    }
                }
            }
            return winner;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            unSeen = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            CActor pnn = new CActor();
            pnn.im = new Bitmap("O.png");
            pnn.c = 'O';
            pnn.x = (w / 3) * 0 + 10;
            pnn.y = (h / 3) * 0 + 10;
            board[0, 0] = pnn;

            /*pnn = new CActor();
            pnn.im = new Bitmap("O.png");
            pnn.c = 'O';
            pnn.x = (w / 3) * 1 + 10;
            pnn.y = (h / 3) * 0 + 10;
            board[1, 0] = pnn;

            pnn = new CActor();
            pnn.im = new Bitmap("O.png");
            pnn.c = 'O';
            pnn.x = (w / 3) * 0 + 10;
            pnn.y = (h / 3) * 1 + 10;
            board[0, 1] = pnn;

            pnn = new CActor();
            pnn.im = new Bitmap("X.png");
            pnn.c = 'X';
            pnn.x = (w / 3) * 1 + 10;
            pnn.y = (h / 3) * 1 + 10;
            board[1, 1] = pnn;

            pnn = new CActor();
            pnn.im = new Bitmap("X.png");
            pnn.c = 'X';
            pnn.x = (w / 3) * 0 + 10;
            pnn.y = (h / 3) * 2 + 10;
            board[0, 2] = pnn;

            pnn = new CActor();
            pnn.im = new Bitmap("O.png");
            pnn.c = 'O';
            pnn.x = (w / 3) * 1 + 10;
            pnn.y = (h / 3) * 2 + 10;
            board[1, 2] = pnn;*/

        }
        void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(this.CreateGraphics());
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(unSeen);
            DrawScene(g2);
            g.DrawImage(unSeen, 0, 0);
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.White);
            Pen pp = new Pen(Color.Black, 3);
            g.DrawLine(pp, w / 3, 0, w/3, h);
            g.DrawLine(pp,(w/3)*2, 0, (w / 3) * 2, h);
            g.DrawLine(pp, 0, h/3, w, h/3);
            g.DrawLine(pp, 0, (h / 3)*2, w, (h / 3) * 2);
            for(int i = 0; i < 3; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (board[i,x] != null)
                    {
                        g.DrawImage(board[i, x].im, board[i,x].x, board[i,x].y);
                    }
                }
            }
        }
    }
    public class CActor
    {
        public int x, y, w, h;
        public int sts = -1;
        public Bitmap im;
        public char c;
    }
    public class Tiles
    {
        public int x, y;
        public char c;
    }
}
