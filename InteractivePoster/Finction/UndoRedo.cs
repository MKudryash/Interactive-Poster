using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace InteractivePoster.Finction
{
    class UndoRedo
    {
        public List<Path> undoP = new List<Path>();
        public List<Path> redoP = new List<Path>();

        public List<Icommands> u = new List<Icommands>();
        public List<Icommands> p = new List<Icommands>();

        public Stack<ICommand> undoCommands = new Stack<ICommand>();
        public Stack<ICommand> redoCommands = new Stack<ICommand>();

        public void Undo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (u.Count != 0)
                {
                    ICommand command = new DrawWithPencilCommand(u[u.Count - 1].figure, u[u.Count - 1].canvas);
                    p.Add(u[u.Count - 1]);
                    removePath(u[u.Count - 1].figure,1);
                    command.UnExecute();      
                }
            }
        }

        public void Redo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (p.Count != 0)
                {
                    ICommand command = new DrawWithPencilCommand(p[p.Count - 1].figure, p[p.Count - 1].canvas);
                    u.Add(p[p.Count - 1]);
                    removePath(p[p.Count - 1].figure,2);
                    command.Execute();
                }
            }
        }

        public void AddComand(ICommand command,Icommands icommands)
        {
            u.Add(icommands);
            p.Clear();
            redoP.Clear();
            undoCommands.Push(command);
            redoCommands.Clear();
        }


        public void removePath(Path path,int ur)
        {
            switch (ur)
            {
                case 1:
                    foreach (var item in u)
                    {
                        if (item.figure == path)
                        {
                            u.Remove(item);
                            break;
                        }
                    }
                    break;
                case 2:
                    foreach (var item in p)
                    {
                        if (item.figure == path)
                        {
                            p.Remove(item);
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }

        }       

    }
}
