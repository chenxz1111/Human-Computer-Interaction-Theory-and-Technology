using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using DynamicPanels;

public class PanelCreator : MonoBehaviour
{
	public DynamicPanelsCanvas canvas;

	public RectTransform content1, content2, content3, content4;
	public string tabLabel1, tabLabel2, tabLabel3, tabLabel4;
	public Sprite tabIcon1, tabIcon2, tabIcon3, tabIcon4;

	void Start()
	{
		// Create 3 panels
		Panel panel1 = PanelUtils.CreatePanelFor(content1, canvas);
		Panel panel2 = PanelUtils.CreatePanelFor(content2, canvas);
		Panel panel3 = PanelUtils.CreatePanelFor(content3, canvas);

		// Add a second tab to the first panel
		panel1.AddTab(content4);

		// Set the labels and the (optional) icons of the tabs
		panel1[0].Icon = tabIcon1; // first tab
		panel1[0].Label = tabLabel1;
		panel1[1].Icon = tabIcon4; // second tab
		panel1[1].Label = tabLabel4;

		panel2[0].Icon = tabIcon2;
		panel2[0].Label = tabLabel2;
		panel3[0].Icon = tabIcon3;
		panel3[0].Label = tabLabel3;

		// Set the minimum sizes of the contents associated with the tabs
		panel1[0].MinSize = new Vector2(200f, 200f); // first tab
		panel1[1].MinSize = new Vector2(200f, 200f); // second tab

		panel2[0].MinSize = new Vector2(200f, 200f);
		panel3[0].MinSize = new Vector2(200f, 200f);

		// Create a vertical panel group
		PanelGroup groupLeftVertical = new PanelGroup(canvas, Direction.Top); // elements are always arranged from bottom to top
		groupLeftVertical.AddElement(panel1); // bottom panel
		groupLeftVertical.AddElement(panel2); // top panel

		// Dock the elements to the Dynamic Panels Canvas (the order is important)
		panel3.DockToRoot(Direction.Bottom);
        groupLeftVertical.DockToRoot(Direction.Left);

        // Rebuild the layout before attempting to resize elements or read their correct sizes/minimum sizes
        canvas.ForceRebuildLayoutImmediate();

        // It is recommended to manually resize layout elements that are created by code and docked.
        // Otherwise, their sizes will not be deterministic. In this case, we are resizing them to their minimum size
        groupLeftVertical.ResizeTo(new Vector2(groupLeftVertical.MinSize.x, groupLeftVertical.Size.y));
        panel3.ResizeTo(new Vector2(panel3.Size.x, panel3.MinSize.y));
	}


}
