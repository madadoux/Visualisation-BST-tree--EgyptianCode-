using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visualisation_BST_tree
{




    public enum d_mode
    {
        preorder, postorder, inorder
    }



    public enum operation
    {

        insert, delete
    }


    public class node
    {

        public node left, right;
        public int val;
        public node(int val)
        {
            this.left = null;
            this.right = null;
            this.val = val;
        }
    }
    public class BST
    {




        public node root { get; internal set; }
        int size;
        int Copy;

        public BST()
        {
            this.root = null;
            this.size = 0;
        }


        public node find(int val)
        {
            node tmp2 = this.root;

            while (tmp2 != null)
            {
                if (val > tmp2.val)
                    tmp2 = tmp2.right;
                else if (val < tmp2.val)
                    tmp2 = tmp2.left;
                else
                    return tmp2;
            }
            return null;
        }










        public bool insert(int val)
        {
            var tmp = new node(val); // ????? ??????? ?????
            var tmp2 = this.root; // ?????? ?? ???? ??? ??? 
            node tmp3 = null;
            if (this.root == null) //?? ???? ?????? ????? 
            {

                this.root = tmp;

            }
            else
            {
                while (tmp2 != null)
                {
                    tmp3 = tmp2;
                    if (val > tmp2.val)
                        tmp2 = tmp2.right;
                    else if (val < tmp2.val)
                        tmp2 = tmp2.left;
                    else
                    {
                        return false;
                    }
                }
                //?????? ? ?????? ??????
                if (val > tmp3.val)
                {

                    tmp3.right = tmp;

                }
                else
                {
                    tmp3.left = tmp;
                }
            }
            this.size++;

            return true;
        }




        public List<string> get_elements(d_mode d)
        {



            List<string> L = new List<string>();
            switch (d)
            {
                case d_mode.preorder:
                    preorder(this.root, ref  L);


                    break;
                case d_mode.postorder:

                    postorder(this.root, ref L);
                    break;
                case d_mode.inorder:


                    inorder(this.root, ref L);
                    break;
                default:
                    break;
            }



            return L;
        }

        bool inorder(node root, ref List<string> values)
        {

            if (this.size == 0)
                return false;

            if (root == null)
                return false;

            this.inorder(root.left, ref values);
            values.Add(root.val.ToString());
            this.inorder(root.right, ref values);



            return true;

        }




        bool preorder(node root, ref List<string> values)
        {

            if (this.size == 0)
                return false;

            if (root == null)
                return false;



            values.Add(root.val.ToString());
            this.inorder(root.left, ref values);

            this.inorder(root.right, ref values);
            return true;
        }


        bool postorder(node root, ref List<string> values)
        {

            if (this.size == 0)
                return false;

            if (root == null)
                return false;


            this.inorder(root.left, ref values);

            this.inorder(root.right, ref values);
            values.Add(root.val.ToString());

            return true;

        }
        public bool removeNode(int val)
        {
            bool found = false;
            node parent = null;
            node tmp = this.root;
            // childCount,
            // replacement,
            bool isRightchild = false;



            //only proceed if the node was found
            while (!found && tmp != null)
            {
                //if the value is less than the tmp node's, go left
                if (val < tmp.val)
                {
                    parent = tmp;
                    tmp = tmp.left;
                    isRightchild = false;
                    //if the value is greater than the current node's, go right
                }
                else if (val > tmp.val)
                {
                    parent = tmp;
                    tmp = tmp.right;
                    isRightchild = true;
                    //values are equal, found it!
                }
                else
                {
                    found = true;
                }
            }
            if (!found)
            {

                return false;
            }
            if (found)
            {
                if ((tmp.left == null) && (tmp.right == null)) //leaf
                {
                    if (parent == null)//delete root
                    {
                        var cur = this.root;
                        {

                            this.root = null;
                        }
                    }
                    else
                    {
                        node cur;
                        //default
                        if (isRightchild)
                        {
                            cur = parent.right;
                            //ClearCircule(cur.x,cur,y,25);

                            parent.right = null;
                        }
                        else
                        {
                            cur = parent.left;
                            //clearline(parent.x,parent.y,cur.x,cur.y);
                            parent.left = null;
                        }
                    }

                    this.size--;
                    return true;
                }

                else if (tmp.left == null)
                {
                    //case root
                    if (parent == null) //delete root
                    {

                        var cur = tmp.right;
                        this.root = tmp.right;
                    }
                    else
                    {
                        //default
                        if (isRightchild)
                        {

                            var cur = tmp.right;

                            parent.right = tmp.right;
                        }
                        else
                        {

                            var cur = tmp.right;

                            parent.left = tmp.right;
                        }
                    }

                    this.size--;

                    return true;
                }

                else if (tmp.right == null)
                {
                    if (parent == null) //delete root
                    {

                        var cur = tmp.left;

                        this.root = tmp.left;
                    }
                    else
                    {
                        //default
                        if (isRightchild)
                        {

                            var cur = tmp.left;

                            parent.right = tmp.left;
                        }
                        else
                        {

                            var cur = tmp.left;

                            parent.left = tmp.left;
                        }
                    }
                    this.size--;

                    return true;
                }

                else // if((tmp->Left!=NULL)&&(tmp->right!=NULL))
                {
                    var maxleft = this.maxVal(tmp.left).val;
                    this.Copy = maxleft;
                    this.removeNode(maxleft);
                    tmp.val = this.Copy;


                }

                return true;
            }

            return false;
        }


        public node maxVal(node where)
        {

            var tmp = where;
            while (tmp != null)
            {
                if (tmp.right == null)
                {
                    return tmp;
                }
                else
                    tmp = tmp.right;
            }


            return null;
        }
        public node minVal(node where)
        {

            var tmp = where;
            while (tmp != null)
            {
                if (tmp.left == null)
                {
                    return tmp;
                }
                else
                    tmp = tmp.left;
            }


            return null;
        }

        public void CLEAR()
        {
            this.clear(this.root);
            this.root = null;
            this.size = 0;
        }
        void clear(node root)
        {
            if (root != null)
            {

                this.clear(root.left);
                this.clear(root.right);
                root = null;
                this.size--;
            }
        }









    }
}
