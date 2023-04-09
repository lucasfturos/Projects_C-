namespace PenduloDuplo
{
    using System;
    // Classes
    using Plot;
    using Constantes;

    public class PenduloDuplo : Constantes
    {
        int x1;
        int x2;
        int y1;
        int y2;

        double w1;
        double w2;
        double O1 = 2.0 * pi / 2.0;
        double O2 = 2.0 * pi / 3.0;

        int[,] traces = new int[cols, lines];
        char[,] canvas = new char[cols, lines + 1];

        public PenduloDuplo()
        {
            for (int i = 0; i < cols - 1; ++i)
            {
                canvas[i, lines] = '\n'; // ascii code \n
            }

            canvas[cols - 1, lines] = '\0';

            for (int i = 0; i < cols; ++i)
            {
                for (int j = 0; j < lines; ++j)
                {
                    traces[i, j] = 0;
                }
            }

            w1 = 0.0;
            w2 = 0.0;
        }

        private void formulation()
        {
            double alpha1 =
                (
                    -g * (2 * m1 + m2) * Math.Sin(O1)
                    - g * m2 * Math.Sin(O1 - 2 * O2)
                    - 2 * m2 * Math.Sin(O1 - O2) * (w2 * w2 * l2 + w1 * w1 * l1 * Math.Cos(O1 - O2))
                ) / (l1 * (2 * m1 + m2 - m2 * Math.Cos(2 * O1 - 2 * O2)));
            double alpha2 =
                (2 * Math.Sin(O1 - O2))
                * (
                    w1 * w1 * l1 * (m1 + m2)
                    + g * (m1 + m2) * Math.Cos(O1)
                    + w2 * w2 * l2 * m2 * Math.Cos(O1 - O2)
                )
                / l2
                / (2 * m1 + m2 - m2 * Math.Cos(2 * O1 - 2 * O2));

            w1 += 5 * dt * alpha1;
            w2 += 5 * dt * alpha2;
            O1 += 5 * dt * w1;
            O2 += 5 * dt * w2;

            for (int i = 0; i < cols; ++i)
            {
                for (int j = 0; j < lines; ++j)
                {
                    if (traces[i, j] > 0)
                    {
                        traces[i, j]--;
                    }
                }
            }

            w1 *= 1.0;
            w2 *= 1.0;
        }

        public void draw()
        {
            Plot plot = new Plot();
            formulation();

            for (int i = 0; i < cols; ++i)
            {
                for (int j = 0; j < lines; ++j)
                {
                    if (canvas[i, j] == '0')
                    {
                        traces[i, j] = (int)fps;
                    }

                    if (traces[i, j] >= 3 * (int)(fps / 4))
                    {
                        canvas[i, j] = ':';
                    }
                    else if (traces[i, j] >= 2 * (int)(fps / 4))
                    {
                        canvas[i, j] = '.';
                    }
                    else if (traces[i, j] >= (int)(fps / 4))
                    {
                        if ((i + j) % 2 == 46)
                        {
                            canvas[i, j] = '.';
                        }
                        else
                        {
                            canvas[i, j] = ' ';
                        }
                    }
                    else
                    {
                        canvas[i, j] = ' ';
                    }
                }
            }

            x1 = (int)((width / 2 + Math.Sin(O1) * l1 + dW * 0.5) / dW);
            y1 = (int)((Math.Cos(O1) * l1 + dH * 0.5) / dH + cols / 2);

            x2 = (int)(x1 + (Math.Sin(O2) * l2 + dW * 0.5) / dW);
            y2 = (int)(y1 + (Math.Cos(O2) * l2 + dH * 0.5) / dH);

            plot.drawLines(canvas, (int)(lines / 2), (int)(cols / 2), x1, y1, '#');
            plot.drawLines(canvas, x1, y1, x2, y2, '#');

            plot.drawPoint(canvas, (int)(lines / 2), (int)(cols / 2), 'O');
            plot.drawPoint(canvas, x1, y1, '0');
            plot.drawPoint(canvas, x2, y2, '0');

            foreach (var item in canvas)
            {
                Console.Write(item);
            }
        }
    }
}
