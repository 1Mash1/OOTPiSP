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
        private List<IDataProcessorPlugin> dataPlugins = new List<IDataProcessorPlugin>(); // List of all loaded encryption/processing plugins
        private IDataProcessorPlugin activeProcessor = null; // Currently selected plugin for file processing (null if none)

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

        private void btnSave_Click(object sender, EventArgs args)
        {
            string shapeTypeName;
            int shapeColorArgb;
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveDialog.Title = "Сохранить проект как...";
                saveDialog.DefaultExt = "txt";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // 1. Collect all shapes data into a string builder first
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (Shape currentShape in globalShapeList.GetList())
                    {
                        shapeTypeName = currentShape.GetType().Name;
                        shapeColorArgb = currentShape.color.ToArgb();
                        sb.AppendLine($"{shapeTypeName}|{currentShape.x}|{currentShape.y}|{currentShape.x2}|{currentShape.y2}|{shapeColorArgb}");
                    }
                    // 2. Convert string to byte array
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
                    // 3. If an encryption plugin is selected, process the data
                    if (activeProcessor != null)
                    {
                        data = activeProcessor.ProcessBeforeSave(data);
                    }
                    File.WriteAllBytes(saveDialog.FileName, data);
                    MessageBox.Show("Файл успешно сохранен!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnLoad_Click(object sender, EventArgs args)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                string content, shapeTypeFromFile;
                openDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openDialog.Title = "Открыть файл";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 1. Read all bytes from file
                        byte[] data = File.ReadAllBytes(openDialog.FileName);
                        // 2. If an encryption plugin is selected, decrypt the data
                        if (activeProcessor != null)
                        {
                            data = activeProcessor.ProcessAfterLoad(data);
                        }
                        // 3. Convert bytes back to string and split into lines
                        content = System.Text.Encoding.UTF8.GetString(data);
                        string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        globalShapeList.GetList().Clear();
                        selectedShape = null;
                        foreach (string currentLine in lines)
                        {
                            string[] lineParts = currentLine.Split('|');
                            if (lineParts.Length < 6)
                                continue;
                            shapeTypeFromFile = lineParts[0];
                            if (factoryRegistry.ContainsKey(shapeTypeFromFile))
                            {
                                Shape restoredShape = factoryRegistry[shapeTypeFromFile].Create();
                                restoredShape.x = int.Parse(lineParts[1]);
                                restoredShape.y = int.Parse(lineParts[2]);
                                restoredShape.x2 = int.Parse(lineParts[3]);
                                restoredShape.y2 = int.Parse(lineParts[4]);
                                restoredShape.color = Color.FromArgb(int.Parse(lineParts[5]));
                                if (shapeStrategies.ContainsKey(restoredShape.GetType()))
                                    restoredShape.DrawStrategy = shapeStrategies[restoredShape.GetType()];

                                globalShapeList.Add(restoredShape);
                            }
                        }
                        canvas.Invalidate();
                        MessageBox.Show("Загрузка успешно завершена!", "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки файла. \n" + ex.Message);
                    }
                }
            }
        }

        private bool IsPluginSecure(string dllPath)
        {
            bool isCorrect = true;
            string currentHash, savedHash, dateStr;
            string sigPath = dllPath + ".sig";
            try
            {
                currentHash = PluginValidator.GetFileHash(dllPath);
                // Check if signature file exists; if not, create it and mark as incorrect
                if (isCorrect && !File.Exists(sigPath))
                {
                    string[] newSig = { currentHash, DateTime.Now.ToString("yyyy-MM-dd") };
                    File.WriteAllLines(sigPath, newSig);
                    MessageBox.Show("Подпись создана. Выберите плагин заново.", "Signature Creation");
                    isCorrect = false;
                }
                // Validate the structure of the signature file
                if (isCorrect)
                {
                    string[] sigLines = File.ReadAllLines(sigPath);
                    if (sigLines.Length < 2)
                    {
                        MessageBox.Show("Ошибка: Неверный формат файла подписи.");
                        isCorrect = false;
                    }
                    else
                    {
                        savedHash = sigLines[0].Trim();
                        dateStr = sigLines[1].Trim();
                        // Compare the current file hash with the saved hash
                        if (currentHash != savedHash)
                        {
                            MessageBox.Show("Ошибка: несовпадение хэшей!", "Проверка подписи");
                            isCorrect = false;
                        }
                        // Verify the activation date of the signature
                        if (isCorrect && DateTime.TryParse(dateStr, out DateTime activationDate))
                        {
                            if (DateTime.Now < activationDate)
                            {
                                MessageBox.Show($"Ошибка: подпись неактивна {activationDate.ToShortDateString()}", "Проверка подписи");
                                isCorrect = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка проверки: " + ex.Message);
                isCorrect = false;
            }

            return isCorrect;
        }
        private void отключитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeProcessor = null;
            MessageBox.Show("Шифрование отключено. Файлы будут сохраняться в обычном виде.");
        }

        private void loadShapeMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "DLL files (*.dll)|*.dll";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!IsPluginSecure(openFileDialog.FileName))
                        return;
                    try
                    {
                        var assembly = System.Reflection.Assembly.LoadFrom(openFileDialog.FileName);
                        foreach (Type type in assembly.GetTypes())
                        {
                            if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                            {
                                IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                                Shape sampleShape = plugin.GetFactory().Create();
                                Type shapeType = sampleShape.GetType();
                                if (!factoryRegistry.ContainsKey(shapeType.Name))
                                {
                                    factoryRegistry.Add(shapeType.Name, plugin.GetFactory());
                                    if (!shapeStrategies.ContainsKey(shapeType))
                                    {
                                        shapeStrategies.Add(shapeType, plugin.GetStrategy());
                                    }
                                    AddPluginButton(plugin);
                                    MessageBox.Show($"Фигура {shapeType.Name} успешно добавлена!");
                                }
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка загрузки фигуры: " + ex.Message); }
                }
            }
        }

        private void loadFuncMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "DLL files (*.dll)|*.dll";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!IsPluginSecure(openFileDialog.FileName))
                        return;
                    try
                    {
                        var assembly = System.Reflection.Assembly.LoadFrom(openFileDialog.FileName);
                        foreach (Type type in assembly.GetTypes())
                        {
                            if (typeof(IDataProcessorPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                            {
                                IDataProcessorPlugin dataPlugin = (IDataProcessorPlugin)Activator.CreateInstance(type);
                                if (!dataPlugins.Any(p => p.Name == dataPlugin.Name))
                                {
                                    dataPlugins.Add(dataPlugin);
                                    ToolStripMenuItem pluginRootItem = new ToolStripMenuItem(dataPlugin.Name);
                                    pluginRootItem.Font = new Font("Comic Sans MS", 12f);
                                    pluginRootItem.BackColor = Color.Orchid;

                                    ToolStripMenuItem selectItem = new ToolStripMenuItem("Activate");
                                    selectItem.Font = new Font("Comic Sans MS", 12f);
                                    selectItem.Click += (s, ev) => { activeProcessor = dataPlugin; MessageBox.Show(dataPlugin.Name + " activated!"); };

                                    ToolStripMenuItem settingsItem = new ToolStripMenuItem("Settings");
                                    settingsItem.Font = new Font("Comic Sans MS", 12f);
                                    settingsItem.Click += (s, ev) => { dataPlugin.ShowSettings(); };

                                    pluginRootItem.DropDownItems.Add(selectItem);
                                    pluginRootItem.DropDownItems.Add(settingsItem);
                                    encryptionToolStripMenuItem.DropDownItems.Add(pluginRootItem);
                                    MessageBox.Show($"Плагин шифрования {dataPlugin.Name} загружен!");
                                }
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка загрузки шифрования: " + ex.Message); }
                }
            }
        }
    }

}