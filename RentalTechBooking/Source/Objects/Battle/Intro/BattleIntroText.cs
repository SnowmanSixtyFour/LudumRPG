using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RentalTechBooking.Source;
using RentalTechBooking.Source.Data;
using RentalTechBooking.Source.Data.Storage;
using RentalTechBooking.Source.Graphics;

namespace RentalTechBooking.Source.Objects.Battle.Intro
{
    internal class BattleIntroText
    {
        // Visuals
        protected StaticSprite
            dialogBox,
            portrait;
        protected Text
            line;

        // Properties
        public bool visible = true;

        // Dialog
        public Dialog dialog;
        private int steps = 0; // Current Line of Dialog
        public bool endOfDialog { get; private set; } = false;
        public int currentLine = 0;
        private Color invisible = (Color.White * 0f);

        // Typewriter
        private String typewriterText = "";
        private bool typewriterFinished = false;
        private int currentChar = 0;
        private float timeElapsed = 0;

        public BattleIntroText()
        {
            line = new Text("", new Vector2(10, 10), Color.Lime);
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // --- Dialog Text ---

            // Update Current Line
            if (currentLine != steps) currentLine = steps;

            // If Dialog is not finished
            if (steps < dialog.Lines.Count)
            {
                // Set Text to Current Line in Typewriter Text
                if (!typewriterFinished)
                {
                    // When Timer Reaches Dialog Speed
                    if (timeElapsed >= Global.dialogSpeed)
                    {
                        // Update Text to Match Current Character
                        typewriterText = dialog.Lines[currentLine].Text.Substring(0, currentChar);

                        // Update Current Character of Dialog
                        currentChar++;

                        SFX.dialog.Play();

                        // Display Text
                        this.line.setText(typewriterText);

                        // Reset Timer
                        timeElapsed = 0f;

                        // When End of Dialog is Reached
                        if (currentChar >= (dialog.Lines[currentLine].Text.Length + 1))
                        {
                            typewriterFinished = true;
                        }
                    }
                }
            }

            // End of Dialog Reached
            else
            {
                endOfDialog = true;
                this.visible = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            line.Draw(spriteBatch);
        }

        public bool isVisible()
        {
            return visible;
        }

        public void Reset()
        {
            ResetTypewriter();
        }

        // Reset Typewriter Variables for Reuse
        private void ResetTypewriter()
        {
            this.currentChar = 0; // Reset Character

            // Reset Text
            typewriterFinished = false;
            typewriterText = "";

            // Reset Timer
            timeElapsed = 0f;
        }

        // Set Dialog
        public void setDialog(Dialog newDialog)
        {
            ResetTypewriter();

            this.steps = 0; // Reset Steps
            this.currentLine = 0; // Reset Current Line
            this.endOfDialog = false;

            this.dialog = newDialog; // Set New Dialog

            // Show Dialog Box
            this.visible = true;
        }

        // Continue Dialog
        public void Proceed()
        {
            // Only advance if dialog is not yet finished
            if (!endOfDialog)
            {
                // Go To Next Line
                if (currentChar >= (dialog.Lines[currentLine].Text.Length + 1))
                {
                    ResetTypewriter();
                    steps++;

                    SFX.dialogContinue.Play();
                }
                // Skip Dialog
                else
                {
                    currentChar = dialog.Lines[currentLine].Text.Length;
                }
            }
        }
    }
}