using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visualisation_BST_tree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            INIT();
        }





        float MAX_X;
        float MAX_Y;

        BST b;

        Stack<KeyValuePair<operation, int>> undo, redo;



        node FoundNode = null;


        void INIT()
        {

            MAX_X = .95f * panel1.Width;

            MAX_Y = NODE_RAD * 4;
            b = new BST();

            undo = new Stack<KeyValuePair<operation, int>>();
            redo = new Stack<KeyValuePair<operation, int>>();


            b.insert(25);
            b.insert(15);
            b.insert(50);
            b.insert(10);
            b.insert(22);
            b.insert(35);
            b.insert(70);
            b.insert(4);
            b.insert(12);
            b.insert(18);
            b.insert(24);
            b.insert(31);
            b.insert(44);
            b.insert(66);
            b.insert(90);
            b.insert(3);
            b.insert(5);
            b.insert(1);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            var val = int.Parse(textBox1.Text);
            b.insert(val);
            tree_update();

            undo.Push(new KeyValuePair<operation, int>(operation.insert, val));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var val = int.Parse(textBox2.Text);
            b.removeNode(val);
            tree_update();


            undo.Push(new KeyValuePair<operation, int>(operation.delete, val));


        }

        void tree_update()
        {
            listView1.Clear();
            var items = b.get_elements(d_mode.inorder);
            foreach (var item in items)
            {

                listView1.Items.Add(item);
            }


            panel1.Refresh();


            var g = panel1.CreateGraphics();

            DrawTree(g, b.root, MAX_X * .05f, MAX_X, MAX_Y, node_type.Head, 1);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tree_update();
        }









        /// <summary>
        /// recursively draw BST  
        /// </summary>
        /// <param name="g"></param> where to draw
        /// <param name="curr"></param> intially the root 
        /// <param name="x"></param>  coordinates 
        /// <param name="y"></param>
        public void DrawTree(Graphics g, node curr, float left, float right, float y, node_type nt, int lvlCount)
        {



            //   Dictionary<PointF, bool> ShapeReg = new Dictionary<PointF, bool>();  

            if (curr == null)
                return;



            var mid = (left + right) / 2;
            DrawTree(g, curr.left, left, mid, y + MAX_Y, node_type.Left, lvlCount + 1);

            DrawTree(g, curr.right, mid, right, y + MAX_Y, node_type.Right, lvlCount + 1);


            drawNode(g, curr, left, right, mid, y, nt, lvlCount);

        }



        Pen node = new Pen(Brushes.YellowGreen),
        nodeTofind = new Pen(Brushes.Violet);



        Pen node_text = new Pen(Brushes.White);


        Pen node_arrow = new Pen(Brushes.Black);



        public void drawNode(Graphics g, node n, float L, float R, float x, float y, node_type nt, int lvlNum)
        {

            var Scale = (4 / 3) * lvlNum;

            float size = NODE_RAD * 2;///  Scale;
            Font drawFont = new Font("Arial", 10);

            switch (nt)
            {
                case node_type.Left:
                    g.DrawLine(node_arrow, x + size / 2, y + size / 2, R + size / 2, y - MAX_Y + size / 2);
                    break;
                case node_type.Right:

                    g.DrawLine(node_arrow, x + size / 2, y + size / 2, L + size / 2, y - MAX_Y + size / 2);
                    break;
                case node_type.Head:
                    break;
                default:
                    break;
            }


            if (n == FoundNode)

                g.FillRectangle(nodeTofind.Brush, x, y, size, size);
            else

                g.FillRectangle(node.Brush, x, y, size, size);




            g.DrawString(n.val.ToString(), drawFont, node_text.Brush, (x + size / 4), y + size / 4);




        }


        public enum node_type
        {
            Left, Right, Head
        }

        float NODE_RAD = 15f;

        private void button3_Click(object sender, EventArgs e)
        {
            FoundNode = b.find(int.Parse(textBox3.Text));
            tree_update();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (undo.Count < 1)
                return;
            var lastOper = undo.Pop();


            switch (lastOper.Key)
            {
                case operation.insert:

                    b.removeNode(lastOper.Value);

                    redo.Push(new KeyValuePair<operation, int>(operation.delete, lastOper.Value));  // oppsite
                    break;
                case operation.delete:


                    b.insert(lastOper.Value);

                    redo.Push(new KeyValuePair<operation, int>(operation.insert, lastOper.Value));  // oppsite
                    break;
                default:
                    break;
            }



            tree_update();

        }

        private void button6_Click(object sender, EventArgs e)
        {


            if (redo.Count < 1)
                return;

            var lastOper = redo.Pop();


            switch (lastOper.Key)
            {
                case operation.insert:

                    b.removeNode(lastOper.Value);

                    undo.Push(new KeyValuePair<operation, int>(operation.delete, lastOper.Value));  // oppsite
                    break;
                case operation.delete:


                    b.insert(lastOper.Value);

                    undo.Push(new KeyValuePair<operation, int>(operation.insert, lastOper.Value));  // oppsite

                    break;
                default:
                    break;
            }



            tree_update();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            b.CLEAR();
            tree_update();
        }
    }
}



 
