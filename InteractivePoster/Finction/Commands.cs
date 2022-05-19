using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace InteractivePoster.Finction
{
  
    interface ICommand
    {
        void Execute();
        void UnExecute();
    }
    public class Icommands
    {

      public  Path figure { get; set; }
       public Canvas canvas { get; set; }
    }

    public class DrawWithPencilCommand : ICommand
    {
        private Path figure;
        private Canvas canvas;


        public DrawWithPencilCommand(Path figure, Canvas canvas)
        {
            this.figure = figure;
            this.canvas = canvas;
        }

        public void Execute()
        {

            canvas.Children.Add(this.figure);
        }

        public void UnExecute()
        {
            canvas.Children.Remove(this.figure);
        }
    }
}
