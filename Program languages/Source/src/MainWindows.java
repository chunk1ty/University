import java.awt.*;
import java.awt.event.*;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map.Entry;

public class MainWindows extends Frame
{	
	private static final long serialVersionUID = 1L;	
	private MenuBar menuBar = new MenuBar();
	private Menu figure = new Menu("Figure");
	private Menu colorMenu = new Menu("Color");
	private Panel panel = new Panel();
	private Button button = new Button("Print result!");	 
	public int currentFigure = 0;
	public Color fillColor;	
	public HashMap<Integer,Figures> hashMap = new HashMap<Integer,Figures>();
	
	public MainWindows() 
	{
		this.setTitle("Graphics Editor");
		this.setResizable(true);
		this.setSize(200, 200);  
		this.setVisible(true);
		
		this.addWindowListener(new WindowAdapter()
		{		
			public void windowClosing(WindowEvent e) 
			{
				System.exit(0);
			}
		});
		
		this.menuBar.add(figure);		
		this.figure.add(new MenuItem("Line"));
		this.figure.add(new MenuItem("Unfilled Rectangle"));
		this.figure.add(new MenuItem("Filled Rectangle"));
		
		this.figure.addActionListener(new ActionListener()
		{
			public void actionPerformed(ActionEvent e)
			{
				String check = e.getActionCommand();
				if (check.equals("Line")) 
				{
					currentFigure = 1; 
				}
				else if (check.equals("Unfilled Rectangle")) 
				{
					currentFigure = 2; 
				}
				else if(check.equals("Filled Rectangle"))
				{
					currentFigure = 3;
				}
				else
				{
					System.out.println("Uncorrect figure!");
				}				
			}
		});
		
		this.menuBar.add(colorMenu);
		this.colorMenu.add(new MenuItem("Black"));
		this.colorMenu.add(new MenuItem("Blue"));
		this.colorMenu.add(new MenuItem("Green"));
		this.colorMenu.add(new MenuItem("Red"));		
		
		this.colorMenu.addActionListener(new ActionListener()
		{
			public void actionPerformed(ActionEvent e)
			{
				String check = e.getActionCommand();
				
				if (check.equals("Black"))
				{
					fillColor = Color.black;
				}
				else if (check.equals("Blue"))
				{
					fillColor = Color.blue;
				}
				else if (check.equals("Green")) 
				{
					fillColor = Color.green;
				}
				else
				{
					fillColor = Color.red;					
				}				
			}
		});		
		
		this.add(button);
		this.add(panel,BorderLayout.SOUTH);
		
		button.addActionListener(new ActionListener()
		{
			public void actionPerformed(ActionEvent event)
			{
				Iterator<Entry<Integer, Figures>> it = hashMap.entrySet().iterator();
				 while(it.hasNext())
				 {
					 int key = it.next().getKey();
					 System.out.println(hashMap.get(key).toString());
				 } 
		}});
		
		this.panel.add(button);		
		this.setMenuBar(menuBar);		
	}	
}		