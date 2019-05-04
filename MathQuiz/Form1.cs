using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        // Create a random number (Is it truly random though?)
        Random randomizer = new Random();

        // ADDITION 
        int addend1;
        int addend2;

        // SUBTRACTION
        int minuend;
        int subtrahend;

        // MULTIPLICATION 
        int multiplicand;
        int multiplier;

        // DIVISION 
        int dividend;
        int divisor;

        // COUNTDOWN
        int timeLeft;

        /* StartTheQuiz()
         * 
         * Starts the quiz by using randomizer to populate the values for
         * addition, subtraction, multiplication, and division
         * 
         * Starts the timer by calling timer1.Start()
         * 
         */
        public void StartTheQuiz()
        {
            // Fill ADDITION
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString(); 
            sum.Value = 0;

            // Fill SUBTRACTION
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill MULTIPLICATION
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill DIVISION
            divisor = randomizer.Next(2, 11);
            // Use a temp variable to make sure
            // that the quotient doesn't have remainders
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            var today = DateTime.Now.ToString("dd MMMM yyyy");
            dateLabel.Text = today;
            dateLabel.Visible = true;
            startButton.Enabled = false;
        }

        /* Timer1_Tick()
         * 
         * If CheckTheAnswer() returns true, then the user 
         * got the answer right. Stop the timer  
         * and show a MessageBox with a happy guy.
         * 
         */
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "(｡◕ ∀ ◕｡)");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";

                if (timeLeft < 6)
                {
                    // Put the Pressure ONNNN!
                    timeLabel.BackColor = Color.Red;
                    // Add some noise to amplify stress
                    SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\Alarm10.wav");
                    simpleSound.Play();
                }
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "(ﾉಥ益ಥ）ﾉ﻿ ┻━┻");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                timeLabel.BackColor = Control.DefaultBackColor;
            }
        }

        /* CheckTheAnswer()
         * 
         * Checks to see if all the answers are correct
         * 
         * If they are then it returns true
         * This causes the first IF statement of TimeTick() to exectue
         * 
         */
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
        && (minuend - subtrahend == difference.Value) && (multiplicand * multiplier == product.Value) && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        /* answer_Check()
         * 
         * ValueChanged event handler for all numericUpDown boxes
         * 
         * Checks to see if the user typed in a correct answer
         * 
         * If they do, it makes a cute chime
         * 
         */
        private void sum_Check(object sender, EventArgs e)
        {
            if (addend1 + addend2 == sum.Value) {
                SoundPlayer correctSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
                correctSound.Play();
            }
        }

        private void diff_Check(object sender, EventArgs e)
        {
            if (minuend - subtrahend == difference.Value)
            {
                SoundPlayer correctSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
                correctSound.Play();
            }
        }

        private void prod_Check(object sender, EventArgs e)
        {
            if (multiplicand * multiplier == product.Value)
            {
                SoundPlayer correctSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
                correctSound.Play();
            }
        }

        private void divi_Check(object sender, EventArgs e)
        {
            if (dividend / divisor == quotient.Value)
            {
                SoundPlayer correctSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
                correctSound.Play();
            }
        }

    }
}
