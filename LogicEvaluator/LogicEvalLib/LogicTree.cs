using System;

namespace LogicEvalLib
{
    public enum CharType
    {
        LeftGrouping,
        RightGrouping,
        Logic,
        Variable,
        Modifier        // like NOT -> '!"
    }

    public class LogicTree
    {
        LogicNode root = null;

        public override string ToString()
        {
            return root.ToString();
        }

        public LogicNode Parse(string parsestring)
        {
            Console.WriteLine("Parsing : " + parsestring);

            // simple error checking...
            if ((parsestring == null) || (parsestring.Length == 0))
                throw new ArgumentException("Logic string is invalid (null or empty).");

            LogicNode currnode = null;
            LogicNode root = null;
            LogicNode last = null;
            LogicNode next = null;
            LogicNode danglingmodifier = null;
            string substr = string.Empty;

            int i = 0;
            next = new LogicNode(parsestring[i]);

            // do some error checking on the first char before we get going...
            if ( (next.Type == CharType.RightGrouping) ||
                 (next.Type == CharType.Logic) )
            {
                throw new ArgumentException("Invalid logic string:  " + parsestring);
            }
            
            while (next != null)
            {
                last = currnode;
                currnode = next;

                // done when next is off the end of the string...
                if (i >= parsestring.Length-1)
                    next = null;
                else
                    next = new LogicNode(parsestring[i+1]);

                switch (currnode.Type)
                {
                    case CharType.Variable:
                        if (null == last)   // just getting started...
                            root = currnode;
                        else if (last.Type == CharType.Logic)  // finishing a subtree...
                        {
                            last.rightchild = currnode;
                            root = last;
                        }
                        else if (last.Type == CharType.LeftGrouping)
                        {
                            currnode.leftchild = last;
                            if (root == null)
                                root = last;
                        }
                        else if (last.Type == CharType.Modifier)
                        {
                            currnode.modifier = last;
                            if (root == null)
                                root = currnode;
                            else
                                root.rightchild = currnode;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid argument order:  last = " + last.ToString() + "; current = " + currnode.ToString());
                        }

                        i++;
                        break;

                    case CharType.Logic:
                        if (last.Type == CharType.Variable)
                        {
                            if (root.Type == CharType.Variable)  // small tree -  might be just getting started...
                                currnode.leftchild = last;
                            else if (root.Type == CharType.Logic)  // make this node the new root - make the old root a child, and 
                                // set this node to be the root.  probably standard case or bigger tree...
                                currnode.leftchild = root;

                            root = currnode;
                        }
                        else if (last.Type == CharType.RightGrouping)   // move that subtree under this new root...
                        {
                            currnode.leftchild = root;
                            root = currnode;                
                        }
                        else
                        {
                            throw new ArgumentException("Invalid argument order:  last = " + last.ToString() + "; current = " + currnode.ToString());
                        }

                        i++;
                        break;

                    case CharType.LeftGrouping :

                        if ( (last != null) && (last.Type == CharType.Modifier) )
                        {
                            currnode.leftchild = last;
                        }

                        // This is the start of a new subtree.  Find the right side of the grouping and parse that into a subtree.
                        // Then, put the '(' and ')' back on as children of the leftmost and rightmost children...
                        substr = FindRightGrouping(parsestring.Substring(i + 1));
                        LogicNode subroot = Parse(substr);

                        // set the '(' as the leftmost child of the subtree...
                        this.FindLeftmostChild(subroot).leftchild = currnode;

                        // set the ')' as the rightmost child of the subtree.  Set i to the correct index...
                        i += substr.Length + 1;
                        currnode = new LogicNode(parsestring[i]);
                        this.FindRightmostChild(subroot).rightchild = currnode;

                        // check for and set dangling modifier on root -> !(p^q)
                        if ( (subroot.Type != CharType.Variable) && (danglingmodifier != null) )
                        {
                            subroot.modifier = danglingmodifier;
                            danglingmodifier = null;
                        }
                        // bump the index for the trail ')'
                        i++;

                        // reset last, curr, next since we've processed the subtree...
                        if (i < parsestring.Length)
                            next = new LogicNode(parsestring[i]);   // set to the next char to be parsed -- first one after the subtree...
                        else
                            next = null;

                        // Figure out where to parent the subtree.
                        if (root == null)
                            root = subroot;
                        else if (root.rightchild == null)
                            root.rightchild = subroot;
                        else
                            throw new ArgumentException("Root.rightchild is expected to be null.");

                        // now that the subtree is correctly built and parented, and the pointers are set to move forward,
                        // confirm that the next char is valid...
                        if (next != null) // the subtree could be the end of the string...
                        {
                            if ((next.Type == CharType.RightGrouping) ||
                                (next.Type == CharType.Modifier))
                            {
                                throw new ArgumentException("Invalid logic string  :" + parsestring);
                            }
                        }

                        break;

                    case CharType.RightGrouping :
                        throw new ArgumentException("Invalid logic string:  " + parsestring);

                    case CharType.Modifier :    // This is the NOT operator.  Need special handling for particular patterns to 
                        // avoid losing a leading '!' so we can put it on the root when appropriate.  Stuff like: "!(..." or
                        // "(!(..." or "...v!(".  Set a variable to hold it and then check later to see if it's set and get it tucked into the tree...
                        if (last == null)  // if a leading '!' (nothing before it)
                            danglingmodifier = currnode;
                        else if (last.Type == CharType.LeftGrouping)    // if a leading "(!(..."
                            danglingmodifier = last;
                        else if (last.Type == CharType.Logic)    // if a leading "...V!(..." -> "(!p^!q)V!(!r^!s)"
                            danglingmodifier = currnode;

                        i++;
                        break;
                }
            }

            // Because the parsing algorithm looks at the current char and the previous char to build the tree, we
            // need to check the final trailing char to ensure we have a fully valid string...
            if ( (currnode.Type == CharType.LeftGrouping) ||
                 (currnode.Type == CharType.Logic) ||
                 (currnode.Type == CharType.Modifier) )
            {
                throw new ArgumentException("Invalid logic string:  " + parsestring);
            }
            return root;
        }


        private LogicNode FindLeftChildBracket(LogicNode root)
        {
            LogicNode temp = root.leftchild;
            LogicNode ret = null;

            while (temp != null)
            {
                if (temp.Type == CharType.LeftGrouping)
                    ret = temp;

                temp = temp.leftchild;
            }

            return ret;
        }

        private LogicNode FindLeftmostChild(LogicNode root)
        {
            LogicNode ret = root;

            while (ret.leftchild != null)
            {
                ret = ret.leftchild;
            }

            return ret;
        }

        private string FindRightGrouping(string parsesubstring)
        {
            int i = 0;
            int substrcount = 0;
            bool done = false;

            while (i < parsesubstring.Length && !done)
            {
                if (parsesubstring[i] == '(')
                    substrcount++;

                else if (parsesubstring[i] == ')')
                {
                    if (substrcount > 0)
                        substrcount--;
                    else
                        done = true;
                }

                i++;
            }

            // don't return the trailing ')' in the string...
            if (done)
                return parsesubstring.Substring(0, i - 1);
            else
                throw new ArgumentException("Invalid logic string:  " + parsesubstring);
        }

        private LogicNode FindRightmostChild(LogicNode root)
        {
            LogicNode ret = root;

            while (ret.rightchild != null)
            {
                ret = ret.rightchild;
            }

            return ret;
        }
    }
}
