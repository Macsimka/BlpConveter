namespace BlpConverter;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        tabControl = new TabControl();
        tabSingleFile = new TabPage();
        groupPreview = new GroupBox();
        pictureBox = new PictureBox();
        groupFileInfo = new GroupBox();
        lblFileInfoText = new Label();
        panelFileSelect = new Panel();
        btnBrowse = new Button();
        lblDropZone = new Label();
        groupConversion = new GroupBox();
        btnConvertToBlp = new Button();
        btnConvertToPng = new Button();
        btnConvertToJpeg = new Button();
        tabBatchConversion = new TabPage();
        groupBatchOptions = new GroupBox();
        btnBatchConvert = new Button();
        btnSelectTarget = new Button();
        btnSelectSource = new Button();
        txtTargetFolder = new TextBox();
        txtSourceFolder = new TextBox();
        lblTargetFolder = new Label();
        lblSourceFolder = new Label();
        progressBar = new ProgressBar();
        lblProgress = new Label();
        tabSettings = new TabPage();
        groupBlpSettings = new GroupBox();
        cmbCompression = new ComboBox();
        lblCompression = new Label();
        chkGenerateMipmaps = new CheckBox();
        chkPreserveAlpha = new CheckBox();
        groupImageSettings = new GroupBox();
        numJpegQuality = new NumericUpDown();
        lblJpegQuality = new Label();
        cmbDefaultFormat = new ComboBox();
        lblDefaultFormat = new Label();
        tabControl.SuspendLayout();
        tabSingleFile.SuspendLayout();
        groupPreview.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
        groupFileInfo.SuspendLayout();
        panelFileSelect.SuspendLayout();
        groupConversion.SuspendLayout();
        tabBatchConversion.SuspendLayout();
        groupBatchOptions.SuspendLayout();
        tabSettings.SuspendLayout();
        groupBlpSettings.SuspendLayout();
        groupImageSettings.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numJpegQuality).BeginInit();
        SuspendLayout();
        // 
        // tabControl
        // 
        tabControl.Controls.Add(tabSingleFile);
        tabControl.Controls.Add(tabBatchConversion);
        tabControl.Controls.Add(tabSettings);
        tabControl.Dock = DockStyle.Fill;
        tabControl.Location = new Point(0, 0);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(1024, 768);
        tabControl.TabIndex = 0;
        // 
        // tabSingleFile
        // 
        tabSingleFile.Controls.Add(groupPreview);
        tabSingleFile.Controls.Add(groupFileInfo);
        tabSingleFile.Controls.Add(panelFileSelect);
        tabSingleFile.Controls.Add(groupConversion);
        tabSingleFile.Location = new Point(4, 29);
        tabSingleFile.Name = "tabSingleFile";
        tabSingleFile.Padding = new Padding(3);
        tabSingleFile.Size = new Size(1016, 735);
        tabSingleFile.TabIndex = 0;
        tabSingleFile.Text = "Single File";
        tabSingleFile.UseVisualStyleBackColor = true;
        // 
        // groupPreview
        // 
        groupPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        groupPreview.Controls.Add(pictureBox);
        groupPreview.Location = new Point(8, 146);
        groupPreview.Name = "groupPreview";
        groupPreview.Size = new Size(650, 580);
        groupPreview.TabIndex = 0;
        groupPreview.TabStop = false;
        groupPreview.Text = "Preview";
        // 
        // pictureBox
        // 
        pictureBox.Dock = DockStyle.Fill;
        pictureBox.Location = new Point(3, 23);
        pictureBox.Name = "pictureBox";
        pictureBox.Size = new Size(644, 554);
        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox.TabIndex = 0;
        pictureBox.TabStop = false;
        // 
        // groupFileInfo
        // 
        groupFileInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        groupFileInfo.Controls.Add(lblFileInfoText);
        groupFileInfo.Location = new Point(664, 146);
        groupFileInfo.Name = "groupFileInfo";
        groupFileInfo.Size = new Size(344, 416);
        groupFileInfo.TabIndex = 1;
        groupFileInfo.TabStop = false;
        groupFileInfo.Text = "File Information";
        // 
        // lblFileInfoText
        // 
        lblFileInfoText.AutoSize = true;
        lblFileInfoText.Dock = DockStyle.Fill;
        lblFileInfoText.Location = new Point(3, 23);
        lblFileInfoText.Name = "lblFileInfoText";
        lblFileInfoText.Padding = new Padding(10);
        lblFileInfoText.Size = new Size(125, 40);
        lblFileInfoText.TabIndex = 0;
        lblFileInfoText.Text = "No file loaded";
        // 
        // panelFileSelect
        // 
        panelFileSelect.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelFileSelect.BackColor = SystemColors.ControlLight;
        panelFileSelect.BorderStyle = BorderStyle.FixedSingle;
        panelFileSelect.Controls.Add(btnBrowse);
        panelFileSelect.Controls.Add(lblDropZone);
        panelFileSelect.Location = new Point(8, 6);
        panelFileSelect.Name = "panelFileSelect";
        panelFileSelect.Size = new Size(1000, 134);
        panelFileSelect.TabIndex = 2;
        // 
        // btnBrowse
        // 
        btnBrowse.Anchor = AnchorStyles.None;
        btnBrowse.Location = new Point(424, 70);
        btnBrowse.Name = "btnBrowse";
        btnBrowse.Size = new Size(150, 40);
        btnBrowse.TabIndex = 1;
        btnBrowse.Text = "Browse...";
        btnBrowse.UseVisualStyleBackColor = true;
        // 
        // lblDropZone
        // 
        lblDropZone.Anchor = AnchorStyles.None;
        lblDropZone.AutoSize = true;
        lblDropZone.Font = new Font("Segoe UI", 12F);
        lblDropZone.Location = new Point(318, 30);
        lblDropZone.Name = "lblDropZone";
        lblDropZone.Size = new Size(357, 28);
        lblDropZone.TabIndex = 0;
        lblDropZone.Text = "Drag and drop files here or click Browse";
        // 
        // groupConversion
        // 
        groupConversion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        groupConversion.Controls.Add(btnConvertToBlp);
        groupConversion.Controls.Add(btnConvertToPng);
        groupConversion.Controls.Add(btnConvertToJpeg);
        groupConversion.Location = new Point(664, 568);
        groupConversion.Name = "groupConversion";
        groupConversion.Size = new Size(344, 155);
        groupConversion.TabIndex = 3;
        groupConversion.TabStop = false;
        groupConversion.Text = "Convert";
        // 
        // btnConvertToBlp
        // 
        btnConvertToBlp.Enabled = false;
        btnConvertToBlp.Location = new Point(26, 108);
        btnConvertToBlp.Name = "btnConvertToBlp";
        btnConvertToBlp.Size = new Size(312, 35);
        btnConvertToBlp.TabIndex = 2;
        btnConvertToBlp.Text = "Convert to BLP";
        btnConvertToBlp.UseVisualStyleBackColor = true;
        // 
        // btnConvertToPng
        // 
        btnConvertToPng.Enabled = false;
        btnConvertToPng.Location = new Point(26, 26);
        btnConvertToPng.Name = "btnConvertToPng";
        btnConvertToPng.Size = new Size(312, 35);
        btnConvertToPng.TabIndex = 0;
        btnConvertToPng.Text = "Convert to PNG";
        btnConvertToPng.UseVisualStyleBackColor = true;
        // 
        // btnConvertToJpeg
        // 
        btnConvertToJpeg.Enabled = false;
        btnConvertToJpeg.Location = new Point(26, 67);
        btnConvertToJpeg.Name = "btnConvertToJpeg";
        btnConvertToJpeg.Size = new Size(312, 35);
        btnConvertToJpeg.TabIndex = 1;
        btnConvertToJpeg.Text = "Convert to JPEG";
        btnConvertToJpeg.UseVisualStyleBackColor = true;
        // 
        // tabBatchConversion
        // 
        tabBatchConversion.Controls.Add(groupBatchOptions);
        tabBatchConversion.Controls.Add(progressBar);
        tabBatchConversion.Controls.Add(lblProgress);
        tabBatchConversion.Location = new Point(4, 29);
        tabBatchConversion.Name = "tabBatchConversion";
        tabBatchConversion.Padding = new Padding(3);
        tabBatchConversion.Size = new Size(1016, 735);
        tabBatchConversion.TabIndex = 1;
        tabBatchConversion.Text = "Batch Conversion";
        tabBatchConversion.UseVisualStyleBackColor = true;
        // 
        // groupBatchOptions
        // 
        groupBatchOptions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        groupBatchOptions.Controls.Add(btnBatchConvert);
        groupBatchOptions.Controls.Add(btnSelectTarget);
        groupBatchOptions.Controls.Add(btnSelectSource);
        groupBatchOptions.Controls.Add(txtTargetFolder);
        groupBatchOptions.Controls.Add(txtSourceFolder);
        groupBatchOptions.Controls.Add(lblTargetFolder);
        groupBatchOptions.Controls.Add(lblSourceFolder);
        groupBatchOptions.Location = new Point(8, 6);
        groupBatchOptions.Name = "groupBatchOptions";
        groupBatchOptions.Size = new Size(1000, 200);
        groupBatchOptions.TabIndex = 0;
        groupBatchOptions.TabStop = false;
        groupBatchOptions.Text = "Batch Conversion Options";
        // 
        // btnBatchConvert
        // 
        btnBatchConvert.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBatchConvert.Location = new Point(800, 150);
        btnBatchConvert.Name = "btnBatchConvert";
        btnBatchConvert.Size = new Size(180, 35);
        btnBatchConvert.TabIndex = 6;
        btnBatchConvert.Text = "Start Conversion";
        btnBatchConvert.UseVisualStyleBackColor = true;
        // 
        // btnSelectTarget
        // 
        btnSelectTarget.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSelectTarget.Location = new Point(900, 95);
        btnSelectTarget.Name = "btnSelectTarget";
        btnSelectTarget.Size = new Size(80, 27);
        btnSelectTarget.TabIndex = 5;
        btnSelectTarget.Text = "Browse...";
        btnSelectTarget.UseVisualStyleBackColor = true;
        // 
        // btnSelectSource
        // 
        btnSelectSource.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSelectSource.Location = new Point(900, 45);
        btnSelectSource.Name = "btnSelectSource";
        btnSelectSource.Size = new Size(80, 27);
        btnSelectSource.TabIndex = 4;
        btnSelectSource.Text = "Browse...";
        btnSelectSource.UseVisualStyleBackColor = true;
        // 
        // txtTargetFolder
        // 
        txtTargetFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTargetFolder.Location = new Point(20, 95);
        txtTargetFolder.Name = "txtTargetFolder";
        txtTargetFolder.ReadOnly = true;
        txtTargetFolder.Size = new Size(874, 27);
        txtTargetFolder.TabIndex = 3;
        // 
        // txtSourceFolder
        // 
        txtSourceFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSourceFolder.Location = new Point(20, 45);
        txtSourceFolder.Name = "txtSourceFolder";
        txtSourceFolder.ReadOnly = true;
        txtSourceFolder.Size = new Size(874, 27);
        txtSourceFolder.TabIndex = 2;
        // 
        // lblTargetFolder
        // 
        lblTargetFolder.AutoSize = true;
        lblTargetFolder.Location = new Point(20, 75);
        lblTargetFolder.Name = "lblTargetFolder";
        lblTargetFolder.Size = new Size(99, 20);
        lblTargetFolder.TabIndex = 1;
        lblTargetFolder.Text = "Target Folder:";
        // 
        // lblSourceFolder
        // 
        lblSourceFolder.AutoSize = true;
        lblSourceFolder.Location = new Point(20, 25);
        lblSourceFolder.Name = "lblSourceFolder";
        lblSourceFolder.Size = new Size(103, 20);
        lblSourceFolder.TabIndex = 0;
        lblSourceFolder.Text = "Source Folder:";
        // 
        // progressBar
        // 
        progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        progressBar.Location = new Point(8, 242);
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(1000, 30);
        progressBar.TabIndex = 1;
        // 
        // lblProgress
        // 
        lblProgress.AutoSize = true;
        lblProgress.Location = new Point(8, 219);
        lblProgress.Name = "lblProgress";
        lblProgress.Size = new Size(68, 20);
        lblProgress.TabIndex = 2;
        lblProgress.Text = "Progress:";
        // 
        // tabSettings
        // 
        tabSettings.Controls.Add(groupBlpSettings);
        tabSettings.Controls.Add(groupImageSettings);
        tabSettings.Location = new Point(4, 29);
        tabSettings.Name = "tabSettings";
        tabSettings.Size = new Size(1016, 735);
        tabSettings.TabIndex = 2;
        tabSettings.Text = "Settings";
        tabSettings.UseVisualStyleBackColor = true;
        // 
        // groupBlpSettings
        // 
        groupBlpSettings.Controls.Add(cmbCompression);
        groupBlpSettings.Controls.Add(lblCompression);
        groupBlpSettings.Controls.Add(chkGenerateMipmaps);
        groupBlpSettings.Controls.Add(chkPreserveAlpha);
        groupBlpSettings.Location = new Point(8, 150);
        groupBlpSettings.Name = "groupBlpSettings";
        groupBlpSettings.Size = new Size(400, 180);
        groupBlpSettings.TabIndex = 1;
        groupBlpSettings.TabStop = false;
        groupBlpSettings.Text = "BLP Settings";
        // 
        // cmbCompression
        // 
        cmbCompression.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCompression.FormattingEnabled = true;
        cmbCompression.Location = new Point(20, 135);
        cmbCompression.Name = "cmbCompression";
        cmbCompression.Size = new Size(360, 28);
        cmbCompression.TabIndex = 3;
        // 
        // lblCompression
        // 
        lblCompression.AutoSize = true;
        lblCompression.Location = new Point(20, 112);
        lblCompression.Name = "lblCompression";
        lblCompression.Size = new Size(149, 20);
        lblCompression.TabIndex = 2;
        lblCompression.Text = "Compression Format:";
        // 
        // chkGenerateMipmaps
        // 
        chkGenerateMipmaps.AutoSize = true;
        chkGenerateMipmaps.Checked = true;
        chkGenerateMipmaps.CheckState = CheckState.Checked;
        chkGenerateMipmaps.Location = new Point(20, 60);
        chkGenerateMipmaps.Name = "chkGenerateMipmaps";
        chkGenerateMipmaps.Size = new Size(157, 24);
        chkGenerateMipmaps.TabIndex = 1;
        chkGenerateMipmaps.Text = "Generate Mipmaps";
        chkGenerateMipmaps.UseVisualStyleBackColor = true;
        // 
        // chkPreserveAlpha
        // 
        chkPreserveAlpha.AutoSize = true;
        chkPreserveAlpha.Checked = true;
        chkPreserveAlpha.CheckState = CheckState.Checked;
        chkPreserveAlpha.Location = new Point(20, 30);
        chkPreserveAlpha.Name = "chkPreserveAlpha";
        chkPreserveAlpha.Size = new Size(129, 24);
        chkPreserveAlpha.TabIndex = 0;
        chkPreserveAlpha.Text = "Preserve Alpha";
        chkPreserveAlpha.UseVisualStyleBackColor = true;
        // 
        // groupImageSettings
        // 
        groupImageSettings.Controls.Add(numJpegQuality);
        groupImageSettings.Controls.Add(lblJpegQuality);
        groupImageSettings.Controls.Add(cmbDefaultFormat);
        groupImageSettings.Controls.Add(lblDefaultFormat);
        groupImageSettings.Location = new Point(8, 6);
        groupImageSettings.Name = "groupImageSettings";
        groupImageSettings.Size = new Size(400, 138);
        groupImageSettings.TabIndex = 0;
        groupImageSettings.TabStop = false;
        groupImageSettings.Text = "Image Export Settings";
        // 
        // numJpegQuality
        // 
        numJpegQuality.Location = new Point(20, 100);
        numJpegQuality.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numJpegQuality.Name = "numJpegQuality";
        numJpegQuality.Size = new Size(360, 27);
        numJpegQuality.TabIndex = 3;
        numJpegQuality.Value = new decimal(new int[] { 95, 0, 0, 0 });
        // 
        // lblJpegQuality
        // 
        lblJpegQuality.AutoSize = true;
        lblJpegQuality.Location = new Point(20, 77);
        lblJpegQuality.Name = "lblJpegQuality";
        lblJpegQuality.Size = new Size(146, 20);
        lblJpegQuality.TabIndex = 2;
        lblJpegQuality.Text = "JPEG Quality (1-100):";
        // 
        // cmbDefaultFormat
        // 
        cmbDefaultFormat.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbDefaultFormat.FormattingEnabled = true;
        cmbDefaultFormat.Location = new Point(20, 47);
        cmbDefaultFormat.Name = "cmbDefaultFormat";
        cmbDefaultFormat.Size = new Size(360, 28);
        cmbDefaultFormat.TabIndex = 1;
        // 
        // lblDefaultFormat
        // 
        lblDefaultFormat.AutoSize = true;
        lblDefaultFormat.Location = new Point(20, 24);
        lblDefaultFormat.Name = "lblDefaultFormat";
        lblDefaultFormat.Size = new Size(162, 20);
        lblDefaultFormat.TabIndex = 0;
        lblDefaultFormat.Text = "Default Output Format:";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1024, 768);
        Controls.Add(tabControl);
        Name = "Form1";
        Text = "BLP Converter";
        tabControl.ResumeLayout(false);
        tabSingleFile.ResumeLayout(false);
        groupPreview.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
        groupFileInfo.ResumeLayout(false);
        groupFileInfo.PerformLayout();
        panelFileSelect.ResumeLayout(false);
        panelFileSelect.PerformLayout();
        groupConversion.ResumeLayout(false);
        tabBatchConversion.ResumeLayout(false);
        tabBatchConversion.PerformLayout();
        groupBatchOptions.ResumeLayout(false);
        groupBatchOptions.PerformLayout();
        tabSettings.ResumeLayout(false);
        groupBlpSettings.ResumeLayout(false);
        groupBlpSettings.PerformLayout();
        groupImageSettings.ResumeLayout(false);
        groupImageSettings.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numJpegQuality).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TabControl tabControl;
    private TabPage tabSingleFile;
    private TabPage tabBatchConversion;
    private TabPage tabSettings;

    // Single File Tab
    private GroupBox groupPreview;
    private PictureBox pictureBox;
    private GroupBox groupFileInfo;
    private Label lblFileInfoText;
    private Panel panelFileSelect;
    private Button btnBrowse;
    private Label lblDropZone;
    private GroupBox groupConversion;
    private Button btnConvertToBlp;
    private Button btnConvertToPng;
    private Button btnConvertToJpeg;

    // Batch Conversion Tab
    private GroupBox groupBatchOptions;
    private Button btnBatchConvert;
    private Button btnSelectTarget;
    private Button btnSelectSource;
    private TextBox txtTargetFolder;
    private TextBox txtSourceFolder;
    private Label lblTargetFolder;
    private Label lblSourceFolder;
    private ProgressBar progressBar;
    private Label lblProgress;

    // Settings Tab
    private GroupBox groupBlpSettings;
    private ComboBox cmbCompression;
    private Label lblCompression;
    private CheckBox chkGenerateMipmaps;
    private CheckBox chkPreserveAlpha;
    private GroupBox groupImageSettings;
    private NumericUpDown numJpegQuality;
    private Label lblJpegQuality;
    private ComboBox cmbDefaultFormat;
    private Label lblDefaultFormat;
}