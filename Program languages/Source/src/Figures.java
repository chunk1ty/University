import java.awt.Color;

public abstract class Figures implements Shape
{
	protected int w = 0;
	protected int h = 0;
	protected int x = 0;
	protected int y = 0;	
	protected int x1 = 0;
	protected int y1 = 0;
	protected int x2 = 0;
	protected int y2 = 0;
	protected Color color;	
	
	public Figures(int x1Initial, int y1Initial, int x2Initial, int y2Initial, Color colorInitial)
	{
		this.x1 = x1Initial;
		this.y1 = y1Initial;
		this.x2 = x2Initial;
		this.y2 = y2Initial;
		this.color = colorInitial;				
	}
			
	public String ColorAsString (String colorAsString)
	{
		if (color == Color.black || color == null)
		{
			colorAsString = " Black";
		}
		else if (color == Color.blue)
		{
			colorAsString = " Blue";
		}
		else if (color == Color.green)
		{
			colorAsString = " Green";
		}
		else if (color == Color.red)
		{
			colorAsString = " Red";
		}
		return colorAsString;
	}
}

