using LudumRPG.Source;
using LudumRPG.Source.Data;
using LudumRPG.Source.Data.Story;
using LudumRPG.Source.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumRPG.Source.Objects.Overworld.UI
{
    internal class DialogHUD
    {
        // Visuals
        protected StaticSprite
            dialogBox,
            portrait;
        protected Text
            name,
            line;

        // Properties
        protected bool visible = false;

        // Dialog
        public Dialog dialog;
        private int steps = 0; // Current Line of Dialog
        public bool endOfDialog { get; private set; } = false;
        public bool
            lowering = false,
            heightening = false;
        public int currentLine = 0;
        private Color invisible = (Color.White * 0f);

        // Typewriter
        private String typewriterText = "";
        private bool typewriterFinished = false;
        private int currentChar = 0;
        private float timeElapsed = 0;

        public DialogHUD()
        {
            dialogBox = new StaticSprite(null, new Rectangle(0, 148, Global.resWidth, Global.resHeight), Color.Black * 0.5f);
            portrait = new StaticSprite(null, new Rectangle(Global.resWidth - 72, 154, 64, 64), invisible);

            name = new Text("", new Vector2(15, 154), Color.White);
            line = new Text("", new Vector2(22, 170), Color.White);
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
                // Set Name
                this.name.setText(dialog.Lines[currentLine].Name);

                // Set Portrait
                if (dialog.Lines[currentLine].Portrait != null)
                {
                    this.portrait.SetTexture(dialog.Lines[currentLine].Portrait);

                    this.portrait.SetColor(Color.White);
                }
                else
                {
                    this.portrait.SetColor(invisible);
                }

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
                this.Hide();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                dialogBox.Draw(spriteBatch);
                portrait.Draw(spriteBatch);

                name.Draw(spriteBatch);
                line.Draw(spriteBatch);
            }
        }

        public void Show()
        {
            this.visible = true;
        }

        public void Hide()
        {
            this.visible = false;
        }

        // Reset Typewriter Variables for Reuse
        public void ResetTypewriter()
        {
            this.currentChar = 0; // Reset Character

            // Bool to Loop Typewriter Effect
            typewriterFinished = false;

            // Reset Text
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

            this.endOfDialog = false; // Set Bool to False

            this.dialog = newDialog; // Set New Dialog

            // Show Dialog Box
            this.Show();
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