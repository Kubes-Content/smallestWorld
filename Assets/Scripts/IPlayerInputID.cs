public interface IPlayerInputID
{
    string GetID();
}

public struct MoveCursorID : IPlayerInputID
{
    public string GetID() => "MoveCursor";
} 

public struct RightClickID : IPlayerInputID
{
    public string GetID() => "OnCursorAltClick0";
} 

public struct MiddleClickID : IPlayerInputID
{
    public string GetID() => "OnCursorAltClick1";
} 