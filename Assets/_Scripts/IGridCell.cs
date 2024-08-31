public interface IGridCell
{
    public void HighlightCell();

    public void UnhighlightCell();

    public GridCell GetGridCell();

    public bool IsCellOccupied();
}
