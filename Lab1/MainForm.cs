namespace MyShape
{
    public partial class MainForm : Form
    {
        private ShapeFactory activeShapeFactory; // The currently selected factory
        private Shape previewShape; // Temporary shape object used while dragging the mouse
        private Shape selectedShape; // Selected shape for editing or moving
        private ShapeList globalShapeList = new ShapeList(); // Collection containing all drawn shapes
        private Color selectedColor = Color.Black; // Currently color
        private bool isResizingMode = false; // Flag to track if the user is currently resizing a shape
        private enum ResizeHandle { None, Left, Right, Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight } // Types of handles
        private ResizeHandle activeResizeHandle = ResizeHandle.None; // Stores which handle is currently grabbed

        // Strategy dictionary to link shapes with their drawing behavior
        private Dictionary<Type, IDrawStrategy> shapeStrategies = new Dictionary<Type, IDrawStrategy> // Maps types to strategies
        {
            { typeof(Line), new LineDrawStrategy() },
            { typeof(Circle), new CircleDrawStrategy() },
            { typeof(MyRectangle), new MyRectangleDrawStrategy() },
            { typeof(Square), new SquareDrawStrategy() },
            { typeof(Elipse), new ElipseDrawStrategy() },
            { typeof(Triangle), new TriangleDrawStrategy() }
        };

        // Factory dictionary for loading objects by type name
        private Dictionary<string, ShapeFactory> factoryRegistry = new Dictionary<string, ShapeFactory> // Maps strings to factories
        {
            { "Line", new LineFactory() },
            { "Circle", new CircleFactory() },
            { "MyRectangle", new RectFactory() },
            { "Square", new SquareFactory() },
            { "Elipse", new ElipseFactory() },
            { "Triangle", new TriangleFactory() }
        };

        public MainForm()
        {
            InitializeComponent();
            btnLine.Tag = new LineFactory();
            btnRectangle.Tag = new RectFactory();
            btnCircle.Tag = new CircleFactory();
            btnSquare.Tag = new SquareFactory();
            btnElipse.Tag = new ElipseFactory();
            btnTriangle.Tag = new TriangleFactory();
        }

        private void OnShapeButtonClick(object sender, EventArgs args)
        {
            Button clickedButton;
            if (sender is Button)
            {
                clickedButton = (Button)sender;
                activeShapeFactory = (ShapeFactory)clickedButton.Tag;
                if (selectedShape != null)
                    selectedShape.IsSelected = false;
                selectedShape = null;
            }
        }
        private void canvas_MouseDown(object sender, MouseEventArgs mouseArgs)
        {
            Shape foundShape; // Variable to store a shape found at the click point
            List<Shape> currentShapes; // Local reference to the list of shapes
            if (selectedShape != null) // Check if a shape is currently selected
            {
                activeResizeHandle = GetHitHandle(selectedShape, mouseArgs.Location);
                if (activeResizeHandle != ResizeHandle.None) // If a handle was hit
                {
                    isResizingMode = true; // Enable resizing mode
                    return; // Exit the method to prevent starting a new shape
                }
            }
            foundShape = null; // Initialize foundShape as null
            currentShapes = globalShapeList.GetList(); // Retrieve the current list of shapes
            for (int i = currentShapes.Count - 1; i >= 0; i--) // Iterate backwards to pick the top-most shape
            {
                if (currentShapes[i].DrawStrategy.ContainsPoint(currentShapes[i], mouseArgs.X, mouseArgs.Y)) // Check collision
                {
                    foundShape = currentShapes[i]; // Store the found shape
                    break;
                }
            }
            if (foundShape != null) // If a shape was clicked
            {
                if (selectedShape != null) // If another shape was previously selected
                    selectedShape.IsSelected = false; // Reset its selection status
                selectedShape = foundShape; // Update selectedShape to the new one
                selectedShape.IsSelected = true; // Mark the new shape as selected
                selectedColor = selectedShape.color; // Sync the global color with the selected shape
                btnSelectColor.BackColor = selectedColor; // Update the color button UI
                previewShape = null; // Ensure no preview is active
            }
            else // If clicked on empty space
            {
                if (selectedShape != null) // If a shape was selected
                    selectedShape.IsSelected = false;
                selectedShape = null; // Clear selection reference
                if (activeShapeFactory != null) // If a drawing tool is active
                {
                    previewShape = activeShapeFactory.Create(); // Create a new shape instance
                    previewShape.DrawStrategy = shapeStrategies[previewShape.GetType()]; // Assign the draw strategy
                    previewShape.x = mouseArgs.X; // Set initial X coordinate
                    previewShape.y = mouseArgs.Y; // Set initial Y coordinate
                    previewShape.x2 = mouseArgs.X; // Set initial end X coordinate
                    previewShape.y2 = mouseArgs.Y; // Set initial end Y coordinate
                    previewShape.color = selectedColor; // Set the current drawing color
                }
            }
            canvas.Invalidate();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs mouseArgs)
        {
            if (isResizingMode && selectedShape != null) // Check if resizing is in progress
            {
                // Mapping mouse movement to specific shape coordinate updates
                switch (activeResizeHandle) // Process different handle directions
                {
                    case ResizeHandle.Left: // Resizing from the left side
                        selectedShape.x = mouseArgs.X; // Update start X
                        break;
                    case ResizeHandle.Right: // Resizing from the right side
                        selectedShape.x2 = mouseArgs.X; // Update end X
                        break;
                    case ResizeHandle.Top: // Resizing from the top
                        selectedShape.y = mouseArgs.Y; // Update start Y
                        break;
                    case ResizeHandle.Bottom: // Resizing from the bottom
                        selectedShape.y2 = mouseArgs.Y; // Update end Y
                        break;
                    case ResizeHandle.TopLeft: // Resizing from the top-left corner
                        selectedShape.x = mouseArgs.X; // Update start X
                        selectedShape.y = mouseArgs.Y; // Update start Y
                        break;
                    case ResizeHandle.TopRight: // Resizing from the top-right corner
                        selectedShape.x2 = mouseArgs.X; // Update end X
                        selectedShape.y = mouseArgs.Y; // Update start Y
                        break;
                    case ResizeHandle.BottomLeft: // Resizing from the bottom-left corner
                        selectedShape.x = mouseArgs.X; // Update start X
                        selectedShape.y2 = mouseArgs.Y; // Update end Y
                        break;
                    case ResizeHandle.BottomRight: // Resizing from the bottom-right corner
                        selectedShape.x2 = mouseArgs.X; // Update end X
                        selectedShape.y2 = mouseArgs.Y; // Update end Y
                        break;
                }
                canvas.Invalidate(); // Refresh canvas
            }
            else if (previewShape != null) // If a new shape is being created
            {
                previewShape.x2 = mouseArgs.X; // Update current end X to mouse position
                previewShape.y2 = mouseArgs.Y; // Update current end Y to mouse position
                canvas.Invalidate(); // Refresh canvas
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs mouseArgs)
        {
            isResizingMode = false; // Disable resizing flag
            activeResizeHandle = ResizeHandle.None; // Reset the active handle
            if (previewShape != null) // If a new shape was being drawn
            {
                globalShapeList.Add(previewShape); // Add the completed shape to the list
                previewShape = null; // Clear the preview reference
                canvas.Invalidate(); // Final refresh of the canvas
            }
        }
        private void canvas_Paint(object sender, PaintEventArgs paintArgs)
        {
            Rectangle visualBounds; // Variable for shape boundary calculation
            int[] xPoints, yPoints; // Arrays for storing handle coordinates
            paintArgs.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (Shape shape in globalShapeList.GetList())
            {
                shape.DrawStrategy.Draw(paintArgs.Graphics, shape);
                if (shape.IsSelected) // If the shape is currently selected
                {
                    visualBounds = shape.DrawStrategy.GetBounds(shape); // Calculate current bounds
                    using (Pen selectionPen = new Pen(Color.Red, 1)) // Create a red pen for the selection box
                    {
                        selectionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; // Set dashed style
                        paintArgs.Graphics.DrawRectangle(selectionPen, visualBounds); // Draw the selection border
                    }
                    xPoints = new int[] { // X-coordinates for the handles
                        visualBounds.Left, visualBounds.Right, visualBounds.Left, visualBounds.Right,
                        visualBounds.Left, visualBounds.Right, visualBounds.Left + visualBounds.Width / 2,
                        visualBounds.Left + visualBounds.Width / 2
                    };
                    yPoints = new int[] { // Y-coordinates for the handles
                        visualBounds.Top, visualBounds.Top, visualBounds.Bottom, visualBounds.Bottom,
                        visualBounds.Top + visualBounds.Height / 2, visualBounds.Top + visualBounds.Height / 2,
                        visualBounds.Top, visualBounds.Bottom
                    };
                    for (int i = 0; i < 8; i++)
                    {
                        paintArgs.Graphics.FillRectangle(Brushes.White, xPoints[i] - 3, yPoints[i] - 3, 6, 6); // Draw handle background
                        paintArgs.Graphics.DrawRectangle(Pens.Black, xPoints[i] - 3, yPoints[i] - 3, 6, 6); // Draw handle border
                    }
                }
            }
            if (previewShape != null && previewShape.DrawStrategy != null) // If previewing a new shape
            {
                previewShape.DrawStrategy.Draw(paintArgs.Graphics, previewShape); // Draw the preview shape
            }
        }

        private ResizeHandle GetHitHandle(Shape shape, Point mouseLocation)
        {
            Rectangle bounds; // Bounds of the shape
            int handleHalfSize; // Half of handle size for hit area calculation
            bounds = shape.DrawStrategy.GetBounds(shape); // Get shape boundaries via strategy
            handleHalfSize = 6; // Set detection sensitivity radius
            bool IsMouseInside(int handleX, int handleY) // Local function for point-in-rect check
            {
                Rectangle hitArea = new Rectangle(handleX - handleHalfSize, handleY - handleHalfSize, handleHalfSize * 2, handleHalfSize * 2); // Define hit box
                return hitArea.Contains(mouseLocation); // Return true if mouse is inside
            }
            if (IsMouseInside(bounds.Left, bounds.Top))
                return ResizeHandle.TopLeft; // Check TopLeft handle
            if (IsMouseInside(bounds.Right, bounds.Top))
                return ResizeHandle.TopRight; // Check TopRight handle
            if (IsMouseInside(bounds.Left, bounds.Bottom))
                return ResizeHandle.BottomLeft; // Check BottomLeft handle
            if (IsMouseInside(bounds.Right, bounds.Bottom))
                return ResizeHandle.BottomRight; // Check BottomRight handle
            if (IsMouseInside(bounds.Left, bounds.Top + bounds.Height / 2))
                return ResizeHandle.Left; // Check Left middle handle
            if (IsMouseInside(bounds.Right, bounds.Top + bounds.Height / 2))
                return ResizeHandle.Right; // Check Right middle handle
            if (IsMouseInside(bounds.Left + bounds.Width / 2, bounds.Top))
                return ResizeHandle.Top; // Check Top middle handle
            if (IsMouseInside(bounds.Left + bounds.Width / 2, bounds.Bottom))
                return ResizeHandle.Bottom; // Check Bottom middle handle
            return ResizeHandle.None; // Return None if no handle hit
        }

        private void btnClearCanvas_Click(object sender, EventArgs args)
        {
            if (selectedShape != null) // If a shape is selected
            {
                globalShapeList.GetList().Remove(selectedShape); // Remove only the selected shape
                selectedShape = null; // Clear selection reference
            }
            else // If nothing is selected
                globalShapeList.GetList().Clear(); // Clear the entire list
            canvas.Invalidate(); // Refresh canvas
        }

        private void btnSelectColor_Click(object sender, EventArgs args)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) // If user clicked OK in the dialog
            {
                selectedColor = colorDialog.Color; // Update global selected color
                btnSelectColor.BackColor = selectedColor; // Update button background to show choice
                if (selectedShape != null) // If a shape is currently selected
                {
                    selectedShape.color = selectedColor; // Change color of the selected shape
                    canvas.Invalidate(); // Refresh canvas to update shape color
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs args)
        {
            string shapeTypeName; // Variable to store class name
            int shapeColorArgb; // Variable to store color in integer format
            using (SaveFileDialog saveDialog = new SaveFileDialog()) // Create save file dialog instance
            {
                saveDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; // Set file extensions
                saveDialog.Title = "Save project as..."; // Set window title
                saveDialog.DefaultExt = "txt"; // Set default extension
                if (saveDialog.ShowDialog() == DialogResult.OK) // If user confirmed saving
                {
                    using (StreamWriter fileWriter = new StreamWriter(saveDialog.FileName)) // Open file for writing
                    {
                        foreach (Shape currentShape in globalShapeList.GetList()) // Loop through all shapes
                        {
                            shapeTypeName = currentShape.GetType().Name; // Get the type name (e.g., "Circle")
                            shapeColorArgb = currentShape.color.ToArgb(); // Convert Color object to ARGB int
                            // Data record: Type|X1|Y1|X2|Y2|Color
                            fileWriter.WriteLine($"{shapeTypeName}|{currentShape.x}|{currentShape.y}|{currentShape.x2}|{currentShape.y2}|{shapeColorArgb}"); // Write line
                        }
                    }
                    MessageBox.Show("File saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show success message
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs args)
        {
            string shapeTypeFromFile; // Variable for type name from file
            string[] lineParts; // Array to hold split segments of the line
            using (OpenFileDialog openDialog = new OpenFileDialog()) // Create open file dialog instance
            {
                openDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; // Set file extensions
                openDialog.Title = "Open saved project"; // Set window title
                if (openDialog.ShowDialog() == DialogResult.OK) // If user confirmed loading
                {
                    globalShapeList.GetList().Clear(); // Wipe current canvas content
                    selectedShape = null; // Reset selection
                    foreach (string currentLine in File.ReadLines(openDialog.FileName)) // Iterate through each file line
                    {
                        lineParts = currentLine.Split('|'); // Split line by pipe separator
                        if (lineParts.Length < 6)
                            continue; // Skip malformed lines
                        shapeTypeFromFile = lineParts[0]; // Extract shape type string
                        if (factoryRegistry.ContainsKey(shapeTypeFromFile)) // Check if type is in registry
                        {
                            Shape restoredShape = factoryRegistry[shapeTypeFromFile].Create(); // Use factory to create instance
                            restoredShape.x = int.Parse(lineParts[1]); // Parse and set start X
                            restoredShape.y = int.Parse(lineParts[2]); // Parse and set start Y
                            restoredShape.x2 = int.Parse(lineParts[3]); // Parse and set end X
                            restoredShape.y2 = int.Parse(lineParts[4]); // Parse and set end Y
                            restoredShape.color = Color.FromArgb(int.Parse(lineParts[5])); // Parse and set color
                            // Re-assign drawing strategy and add to global list
                            if (shapeStrategies.ContainsKey(restoredShape.GetType())) // Check for corresponding strategy
                                restoredShape.DrawStrategy = shapeStrategies[restoredShape.GetType()]; // Set the strategy

                            globalShapeList.Add(restoredShape); // Add restored shape to the collection
                        }
                    }
                    canvas.Invalidate(); // Refresh canvas to show loaded data
                    MessageBox.Show("Loading complete!", "Load", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show success message
                }
            }
        }

        private void AddPluginButton(IPlugin plugin)
        {
            string iconName;
            Button newBtn = new Button();
            newBtn.Size = new Size(45, 45); // Size for button
            newBtn.BackColor = Color.LightSkyBlue;
            newBtn.FlatStyle = FlatStyle.Flat;
            newBtn.FlatAppearance.BorderColor = Color.White;
            newBtn.BackgroundImageLayout = ImageLayout.Zoom; 
            newBtn.Tag = plugin.GetFactory();
            newBtn.Text = "";
            iconName = plugin.Name.ToLower();
            object iconObj = Properties.Resources.ResourceManager.GetObject(iconName);
            if (iconObj is Image)
                newBtn.BackgroundImage = (Image)iconObj;
            else
            {
                newBtn.Text = plugin.Name;
                newBtn.Font = new Font("Arial", 7);
            }
            newBtn.Click += OnShapeButtonClick;
            flowPanel.Controls.Add(newBtn);
        }
        private void btnInstallPlugin_Click(object sender, EventArgs e)
        {
            string dllPath, sigPath, dateStr, savedHash, currentHash, folder;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Plugins (*.dll)|*.dll";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dllPath = openFileDialog.FileName;
                // Create the .sig file in the same folder
                sigPath = dllPath + ".sig";
                try
                {
                    // Calculate hash using our validator class
                    currentHash = PluginValidator.GetFileHash(dllPath);
                    if (!File.Exists(sigPath))
                    {
                        // Create content: Hash on line 1, Date on line 2
                        string[] newSig = { currentHash, DateTime.Now.ToString("yyyy-MM-dd") };
                        File.WriteAllLines(sigPath, newSig);
                        folder = Path.GetDirectoryName(sigPath);
                        MessageBox.Show($"Signature created in folder:\n{folder}\n\nPlease select the DLL again to verify.", "Signing Success");
                        return;
                    }
                    string[] sigLines = File.ReadAllLines(sigPath);
                    if (sigLines.Length < 2)
                    {
                        MessageBox.Show("Error: Signature file is invalid.");
                        return;
                    }
                    savedHash = sigLines[0].Trim();
                    dateStr = sigLines[1].Trim();
                    if (currentHash != savedHash)
                    {
                        MessageBox.Show("Plugin integrity violation! Hashes do not match.", "Security Alert");
                        return;
                    }
                    if (DateTime.TryParse(dateStr, out DateTime activationDate))
                    {
                        if (DateTime.Now < activationDate)
                        {
                            MessageBox.Show($"Error: Plugin is not active yet!\nActivation date: {activationDate.ToShortDateString()}", "Security Alert");
                            return;
                        }
                    }
                    LoadPlugin(dllPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Security check failed: " + ex.Message);
                }
            }
        }

        private void LoadPlugin(string dllPath) // Method to handle DLL loading
        {
            bool pluginFound;
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(dllPath);
                pluginFound = false;
                foreach (Type type in assembly.GetTypes())
                {
                    // Check if type implements IPlugin
                    if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                        Shape sampleShape = plugin.GetFactory().Create();
                        Type shapeType = sampleShape.GetType();
                        // Register plugin in dictionaries
                        if (!factoryRegistry.ContainsKey(shapeType.Name))
                        {
                            factoryRegistry.Add(shapeType.Name, plugin.GetFactory());
                            shapeStrategies.Add(shapeType, plugin.GetStrategy());
                            AddPluginButton(plugin); // Add button to UI
                            pluginFound = true;
                        }
                    }
                }
                if (pluginFound) MessageBox.Show("Plugin loaded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading error: " + ex.Message);
            }
        }

    }
}