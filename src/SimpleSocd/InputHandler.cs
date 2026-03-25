using MonoMod.Cil;

namespace OneLetterShor.SimpleSocd;

public static class InputHandler
{
    internal static void ApplyHooksAndEvents()
    {
        IL.RWInput.PlayerInputLogic_int_int += IL_RWInput_PlayerInputLogic_int_int;
        On.RainWorldGame.GrafUpdate += On_RainWorldGame_GrafUpdate; 
    }
    
    private static void On_RainWorldGame_GrafUpdate(On.RainWorldGame.orig_GrafUpdate orig, RainWorldGame rainWorldGame, float timeStacker)
    {
        UpdateInputs();
        
        orig(rainWorldGame, timeStacker);
    }
    
    private static void IL_RWInput_PlayerInputLogic_int_int(ILContext il)
    {
        try
        {
            ILCursor cursor = new(il);

            // TODO: Change il cursor position to emit calls directly after the inputPackage is instantiated.
            cursor.GotoNext(
                MoveType.Before,
                x => x.MatchRet() // end of method
            );

            cursor.EmitDelegate((Player.InputPackage inputPackage) =>
            {
                UpdateInputs();
                return GetEditedInputPackage(inputPackage);
            });
        }
        catch (Exception exception)
        {
            Logger.Fatal(exception);
        }
    }
    
    private static void UpdateInputActions()
    {
        // if (Input.GetKey(Options.LastWinUpKey.Value))
        //     PressedLastWins |= InputActions.Up;
        // else
        //     PressedLastWins &= ~InputActions.Up;
        // if (Input.GetKey(Options.LastWinDownKey.Value))
        //     PressedLastWins |= InputActions.Down;
        // else
        //     PressedLastWins &= ~InputActions.Down;
        // if (Input.GetKey(Options.LastWinRightKey.Value))
        //     PressedLastWins |= InputActions.Right;
        // else
        //     PressedLastWins &= ~InputActions.Right;
        // if (Input.GetKey(Options.LastWinLeftKey.Value))
        //     PressedLastWins |= InputActions.Left;
        // else
        //     PressedLastWins &= ~InputActions.Left;
        //
        // if (Input.GetKey(Options.NeutralUpKey.Value))
        //     PressedNeutrals |= InputActions.Up;
        // else
        //     PressedNeutrals &= ~InputActions.Up;
        // if (Input.GetKey(Options.NeutralDownKey.Value))
        //     PressedNeutrals |= InputActions.Down;
        // else
        //     PressedNeutrals &= ~InputActions.Down;
        // if (Input.GetKey(Options.NeutralRightKey.Value))
        //     PressedNeutrals |= InputActions.Right;
        // else
        //     PressedNeutrals &= ~InputActions.Right;
        // if (Input.GetKey(Options.NeutralLeftKey.Value))
        //     PressedNeutrals |= InputActions.Left;
        // else
        //     PressedNeutrals &= ~InputActions.Left;
    }
    
    public static void UpdateInputs()
    {
        UpdateInputActions();
        
        // if (IsUpPressed && !IsDownPressed)
        // {
        //     Y = 1;
        //     LongestY = 1;
        // }
        // if (!IsUpPressed && IsDownPressed)
        // {
        //     Y = -1;
        //     LongestY = -1;
        // }
        // if (IsUpPressed && IsDownPressed)
        // {
        //     if(LongestY == 0) LogWarning("up and down pressed on same update. not a bug, just information that could be useful.");
        //     
        //     if (!IsVerticalNeutralSocd)
        //         Y = LongestY * -1;
        //     else
        //         Y = 0;
        // }
        // if (!IsUpPressed && !IsDownPressed)
        // {
        //     Y = 0;
        //     LongestY = 0;
        // }
        //
        // if (IsRightPressed && !IsLeftPressed)
        // { 
        //     X = 1;
        //     LongestX = 1;
        // }
        // if (!IsRightPressed && IsLeftPressed)
        // {
        //     X = -1;
        //     LongestX = -1;
        // }
        // if (IsRightPressed && IsLeftPressed)
        // {
        //     if(LongestX == 0) LogWarning("right and left pressed on same update. not a bug, just information that could be useful.");
        //     
        //     if (!IsHorizontalNeutralSocd)
        //         X = LongestX * -1;
        //     else
        //         X = 0;
        // }
        // if(!IsRightPressed && !IsLeftPressed)
        // {
        //     X = 0;
        //     LongestX = 0;
        // }
        //
        //
        // if (Y == -1)
        //     DownDiagonal = X;
        // else
        //     DownDiagonal = 0;
        //
        // AnalogDir.Set(X, Y);
        // AnalogDir.Normalize();
    }
    
    public static Player.InputPackage GetEditedInputPackage(Player.InputPackage inputPackage)
    {
        // UpdateInputs();
        // inputPackage.x = X;
        // inputPackage.y = Y;
        // inputPackage.analogueDir.x = AnalogDir.x;
        // inputPackage.analogueDir.y = AnalogDir.y;
        // if(Y == -1) inputPackage.downDiagonal = DownDiagonal; // if(Y == -1) prevents interference with FastRollButton
        //
        // if (Input.GetKey(KeyCode.J))
        // {
        //     inputPackage.pckp = true;
        //     inputPackage.jmp = true;
        // }
        //
        return inputPackage;
    }
}