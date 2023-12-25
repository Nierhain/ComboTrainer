using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComboTrainer;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private SpriteFont font;
    private List<string> _lastInput;
    private float _leftYAxis;
    private float _leftXAxis;
    private bool _isConnected;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _lastInput = new List<string>();
        _leftYAxis = 0f;
        _leftXAxis = 0f;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        font = Content.Load<SpriteFont>("Text");
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        var padState = GamePad.GetState(PlayerIndex.One);
        if (gameTime.ElapsedGameTime.Milliseconds < 1000 / 60)
        {
            base.Update(gameTime);
            return;
        }
        if (_lastInput.Count > 10) _lastInput.Clear();
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _isConnected = padState.IsConnected;
        // TODO: Add your update logic here
        if (padState.Buttons.A == ButtonState.Pressed) _lastInput.Add("A");
        if (padState.Buttons.B == ButtonState.Pressed) _lastInput.Add("B");
        if (padState.Buttons.X == ButtonState.Pressed) _lastInput.Add("X");
        if (padState.Buttons.Y == ButtonState.Pressed) _lastInput.Add("Y");
        if (padState.Buttons.LeftShoulder == ButtonState.Pressed) _lastInput.Add("LB");
        if (padState.Triggers.Left >= 1f) _lastInput.Add("LT");
        if (padState.Buttons.RightShoulder == ButtonState.Pressed) _lastInput.Add("RB");
        if (padState.Triggers.Right >= 1f) _lastInput.Add("RT");
        _leftYAxis = padState.ThumbSticks.Left.Y;
        _leftXAxis = padState.ThumbSticks.Left.X;
        if (_leftXAxis <= -1f && _leftYAxis == 0f) _lastInput.Add("4");
        if (_leftXAxis >= 1f && _leftYAxis == 0f) _lastInput.Add("6");
        if (_leftYAxis >= 1f && _leftXAxis == 0f) _lastInput.Add("8");
        if (_leftYAxis <= -1f && _leftXAxis == 0f) _lastInput.Add("2");
        if (_leftYAxis <= -1f && _leftXAxis <= -1f) _lastInput.Add("1");
        if (_leftYAxis >= 1f && _leftXAxis <= -1f) _lastInput.Add("7");
        if (_leftYAxis >= 1f && _leftXAxis >= 1f) _lastInput.Add("9");
        if (_leftYAxis <= -1f && _leftXAxis >= 1f) _lastInput.Add("3");
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        var inputs = string.Join(' ', _lastInput);
        _spriteBatch.Begin();
        _spriteBatch.DrawString(font, $"Last input: {inputs}", new Vector2(100, 100), Color.White);
        _spriteBatch.DrawString(font, $"Gamepad: {_isConnected.ToString()}", new Vector2(10, 10), Color.White);
        _spriteBatch.DrawString(font, $"Y-Axis: {_leftYAxis} | X-Axis : {_leftXAxis}", new Vector2(10, 30), Color.White);
        _spriteBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
