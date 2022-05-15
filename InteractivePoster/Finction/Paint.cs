using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Path = System.Windows.Shapes.Path;

namespace InteractivePoster.Finction
{
    class Paint : GeometricPatterns
    {
        public Point startPoint;
        public  Brush currentBrush = Brushes.Black;
        PathFigure currentFigure;
        public Path currentPath = null;
        List<Path> pathFigure { get; set; } = new List<Path>();
        public void GetBrush(Brush Cb)
        {
            currentBrush = Cb;
        }


        public Paint(Canvas cv)
        {
            this.cv = cv;
        }
        public void BuildPoint(MouseEventArgs e)
        {

            double x = e.GetPosition(cv).X;
            double y = e.GetPosition(cv).Y;

            try
            {
                Point ppp = new Point(x, y);
                currentFigure.Segments.Add(new LineSegment(ppp, isStroked: true));
                currentPath.Data = new PathGeometry() { Figures = { currentFigure } };
                cv.Children.Add(currentPath);
                pathFigure.Add(currentPath);
            }
            catch (Exception)
            {

            }

        }

        public void ClearAll()
        {
            foreach (var item in pathFigure)
            {
                cv.Children.Remove(item);
            }
            pathFigure.Clear();
        }

        public void Undo()
        {
            undo_redo.Undo(1);
        }
        public void Redo()
        {
            undo_redo.Redo(1);
        }


        public void StartDraw(MouseEventArgs e)
        {

            double x = e.GetPosition(cv).X;
            double y = e.GetPosition(cv).Y;

            startPoint = new Point(x, y);
            currentFigure = new PathFigure() { StartPoint = startPoint };
            Path path = new Path()
            {
                Stroke = currentBrush,
                StrokeThickness = 3,
                Data = new PathGeometry() { Figures = { currentFigure } }
            };
            cv.Children.Add(path);
            currentPath = path;
            pathFigure.Add(currentPath);
        }
        UndoRedo undo_redo = new UndoRedo();
        public void rr()
        {
            DrawWithPencilCommand command = new DrawWithPencilCommand(currentPath, cv);
            currentPath.MouseLeftButtonDown += RemoveObj;
            undo_redo.AddComand(command);
            currentFigure = null;
            currentPath = null;
        }
        DrawWithPencilCommand commands;
        public void RemoveObj(object sender, MouseEventArgs e)
        {
            if (MaxMinCoordinat.Eraser)
            {
                var Path = sender as Path;
                if (Path == null)
                    return;
                commands = new DrawWithPencilCommand(Path, cv);
                commands.UnExecute();
            }

        }
    }
}
