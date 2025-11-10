using BlpConverter.BLP;
using BlpConverter.Config;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace BlpConverter;

public partial class Form1 : Form
{
    private AppConfig config;
    private string currentFilePath;
    private BlpFile currentBlpFile;

    public Form1()
    {
        InitializeComponent();
        InitializeApp();
    }

    private void InitializeApp()
    {
        // Load configuration
        config = AppConfig.Load();

        // Setup drag and drop
        panelFileSelect.AllowDrop = true;
        panelFileSelect.DragEnter += PanelFileSelect_DragEnter;
        panelFileSelect.DragDrop += PanelFileSelect_DragDrop;

        // Setup event handlers
        btnBrowse.Click += BtnBrowse_Click;
        btnConvertToPng.Click += BtnConvertToPng_Click;
        btnConvertToJpeg.Click += BtnConvertToJpeg_Click;
        btnConvertToBlp.Click += BtnConvertToBlp_Click;
        btnSelectSource.Click += BtnSelectSource_Click;
        btnSelectTarget.Click += BtnSelectTarget_Click;
        btnBatchConvert.Click += BtnBatchConvert_Click;

        // Setup settings controls
        cmbDefaultFormat.Items.AddRange(new object[] { "PNG", "JPEG" });
        cmbDefaultFormat.SelectedIndex = (int)config.ConversionSettings.DefaultOutputFormat;
        cmbDefaultFormat.SelectedIndexChanged += (s, e) =>
        {
            config.ConversionSettings.DefaultOutputFormat = (ImageFormat)cmbDefaultFormat.SelectedIndex;
            config.Save();
        };

        cmbCompression.Items.AddRange(new object[] { "DXT1", "DXT3", "DXT5", "Uncompressed" });
        cmbCompression.SelectedIndex = (int)config.ConversionSettings.BlpCompressionFormat;
        cmbCompression.SelectedIndexChanged += (s, e) =>
        {
            config.ConversionSettings.BlpCompressionFormat = (BlpCompressionFormat)cmbCompression.SelectedIndex;
            config.Save();
        };

        numJpegQuality.Value = config.ConversionSettings.JpegQuality;
        numJpegQuality.ValueChanged += (s, e) =>
        {
            config.ConversionSettings.JpegQuality = (int)numJpegQuality.Value;
            config.Save();
        };

        chkGenerateMipmaps.Checked = config.ConversionSettings.GenerateMipmaps;
        chkGenerateMipmaps.CheckedChanged += (s, e) =>
        {
            config.ConversionSettings.GenerateMipmaps = chkGenerateMipmaps.Checked;
            config.Save();
        };

        chkPreserveAlpha.Checked = config.ConversionSettings.PreserveAlpha;
        chkPreserveAlpha.CheckedChanged += (s, e) =>
        {
            config.ConversionSettings.PreserveAlpha = chkPreserveAlpha.Checked;
            config.Save();
        };

        // Set last used folders
        txtSourceFolder.Text = config.LastSourceFolder;
        txtTargetFolder.Text = config.LastTargetFolder;
    }

    private void PanelFileSelect_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effect = DragDropEffects.Copy;
        }
    }

    private void PanelFileSelect_DragDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                LoadFile(files[0]);
            }
        }
    }

    private void BtnBrowse_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog
        {
            Filter = "Image Files|*.blp;*.png;*.jpg;*.jpeg|BLP Files|*.blp|PNG Files|*.png|JPEG Files|*.jpg;*.jpeg|All Files|*.*",
            Title = "Select an image file"
        };

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            LoadFile(ofd.FileName);
        }
    }

    private void LoadFile(string filePath)
    {
        try
        {
            currentFilePath = filePath;
            string ext = Path.GetExtension(filePath).ToLowerInvariant();

            // Clear previous file
            currentBlpFile?.Dispose();
            currentBlpFile = null;
            pictureBox.Image?.Dispose();
            pictureBox.Image = null;

            if (ext == ".blp")
            {
                LoadBlpFile(filePath);
            }
            else if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
            {
                LoadImageFile(filePath);
            }
            else
            {
                MessageBox.Show("Unsupported file format. Please select a BLP, PNG, or JPEG file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadBlpFile(string filePath)
    {
        using var fs = File.OpenRead(filePath);
        currentBlpFile = new BlpFile(fs);

        // Load preview
        var bitmap = currentBlpFile.GetBitmap(0);
        pictureBox.Image = bitmap;

        // Display file info
        var info = new System.Text.StringBuilder();
        info.AppendLine($"File: {Path.GetFileName(filePath)}");
        info.AppendLine($"Format: BLP");
        info.AppendLine();
        info.AppendLine($"Dimensions: {currentBlpFile.Width}x{currentBlpFile.Height}");
        info.AppendLine($"Mipmaps: {currentBlpFile.MipMapCount}");
        info.AppendLine();
        info.AppendLine($"Encoding: {currentBlpFile.ColorEncoding}");
        info.AppendLine($"Pixel Format: {currentBlpFile.PixelFormat}");
        info.AppendLine($"Alpha Depth: {currentBlpFile.AlphaDepth} bit");
        info.AppendLine();
        info.AppendLine($"File Size: {new FileInfo(filePath).Length / 1024.0:F2} KB");

        lblFileInfoText.Text = info.ToString();

        // Enable conversion buttons
        btnConvertToPng.Enabled = true;
        btnConvertToJpeg.Enabled = true;
        btnConvertToBlp.Enabled = false;
    }

    private void LoadImageFile(string filePath)
    {
        using var image = SixLabors.ImageSharp.Image.Load(filePath);
        var bitmap = new System.Drawing.Bitmap(image.Width, image.Height);

        using (var ms = new MemoryStream())
        {
            image.SaveAsPng(ms);
            ms.Position = 0;
            pictureBox.Image = System.Drawing.Image.FromStream(ms);
        }

        // Display file info
        var info = new System.Text.StringBuilder();
        info.AppendLine($"File: {Path.GetFileName(filePath)}");
        info.AppendLine($"Format: {Path.GetExtension(filePath).TrimStart('.').ToUpperInvariant()}");
        info.AppendLine($"Size: {image.Width}x{image.Height}");
        info.AppendLine($"File Size: {new FileInfo(filePath).Length / 1024.0:F2} KB");

        lblFileInfoText.Text = info.ToString();

        // Enable conversion buttons
        btnConvertToPng.Enabled = false;
        btnConvertToJpeg.Enabled = false;
        btnConvertToBlp.Enabled = true;
    }

    private void BtnConvertToPng_Click(object sender, EventArgs e)
    {
        if (currentBlpFile == null) return;

        using var sfd = new SaveFileDialog
        {
            Filter = "PNG Files|*.png",
            FileName = Path.GetFileNameWithoutExtension(currentFilePath) + ".png"
        };

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            try
            {
                var image = currentBlpFile.GetImage(0);
                image.SaveAsPng(sfd.FileName);
                MessageBox.Show("File converted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void BtnConvertToJpeg_Click(object sender, EventArgs e)
    {
        if (currentBlpFile == null) return;

        using var sfd = new SaveFileDialog
        {
            Filter = "JPEG Files|*.jpg",
            FileName = Path.GetFileNameWithoutExtension(currentFilePath) + ".jpg"
        };

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            try
            {
                var image = currentBlpFile.GetImage(0);
                image.SaveAsJpeg(sfd.FileName, new JpegEncoder { Quality = config.ConversionSettings.JpegQuality });
                MessageBox.Show("File converted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void BtnConvertToBlp_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(currentFilePath)) return;

        using var sfd = new SaveFileDialog
        {
            Filter = "BLP Files|*.blp",
            FileName = Path.GetFileNameWithoutExtension(currentFilePath) + ".blp"
        };

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            try
            {
                BlpWriter.SaveBlp(currentFilePath, sfd.FileName, config.ConversionSettings);
                MessageBox.Show("File converted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void BtnSelectSource_Click(object sender, EventArgs e)
    {
        using var fbd = new FolderBrowserDialog
        {
            Description = "Select source folder",
            SelectedPath = config.LastSourceFolder
        };

        if (fbd.ShowDialog() == DialogResult.OK)
        {
            txtSourceFolder.Text = fbd.SelectedPath;
            config.LastSourceFolder = fbd.SelectedPath;
            config.Save();
        }
    }

    private void BtnSelectTarget_Click(object sender, EventArgs e)
    {
        using var fbd = new FolderBrowserDialog
        {
            Description = "Select target folder",
            SelectedPath = config.LastTargetFolder
        };

        if (fbd.ShowDialog() == DialogResult.OK)
        {
            txtTargetFolder.Text = fbd.SelectedPath;
            config.LastTargetFolder = fbd.SelectedPath;
            config.Save();
        }
    }

    private async void BtnBatchConvert_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSourceFolder.Text) || !Directory.Exists(txtSourceFolder.Text))
        {
            MessageBox.Show("Please select a valid source folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtTargetFolder.Text) || !Directory.Exists(txtTargetFolder.Text))
        {
            MessageBox.Show("Please select a valid target folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        btnBatchConvert.Enabled = false;
        progressBar.Value = 0;

        try
        {
            var files = Directory.GetFiles(txtSourceFolder.Text, "*.*", SearchOption.AllDirectories)
                .Where(f => new[] { ".blp", ".png", ".jpg", ".jpeg" }.Contains(Path.GetExtension(f).ToLowerInvariant()))
                .ToArray();

            progressBar.Maximum = files.Length;

            for (int i = 0; i < files.Length; i++)
            {
                var sourceFile = files[i];
                var relativePath = Path.GetRelativePath(txtSourceFolder.Text, sourceFile);
                var ext = Path.GetExtension(sourceFile).ToLowerInvariant();

                string targetFile;
                if (ext == ".blp")
                {
                    // Convert BLP to PNG/JPEG
                    var targetExt = config.ConversionSettings.DefaultOutputFormat == ImageFormat.Png ? ".png" : ".jpg";
                    targetFile = Path.Combine(txtTargetFolder.Text, Path.ChangeExtension(relativePath, targetExt));
                }
                else
                {
                    // Convert PNG/JPEG to BLP
                    targetFile = Path.Combine(txtTargetFolder.Text, Path.ChangeExtension(relativePath, ".blp"));
                }

                // Create target directory if needed
                var targetDir = Path.GetDirectoryName(targetFile);
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }

                // Convert file
                await Task.Run(() =>
                {
                    if (ext == ".blp")
                    {
                        using var fs = File.OpenRead(sourceFile);
                        using var blp = new BlpFile(fs);
                        var image = blp.GetImage(0);

                        if (config.ConversionSettings.DefaultOutputFormat == ImageFormat.Png)
                        {
                            image.SaveAsPng(targetFile);
                        }
                        else
                        {
                            image.SaveAsJpeg(targetFile, new JpegEncoder { Quality = config.ConversionSettings.JpegQuality });
                        }
                    }
                    else
                    {
                        BlpWriter.SaveBlp(sourceFile, targetFile, config.ConversionSettings);
                    }
                });

                progressBar.Value = i + 1;
                lblProgress.Text = $"Progress: {i + 1}/{files.Length}";
                Application.DoEvents();
            }

            MessageBox.Show($"Batch conversion completed! Converted {files.Length} files.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error during batch conversion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnBatchConvert.Enabled = true;
            progressBar.Value = 0;
            lblProgress.Text = "Progress:";
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        currentBlpFile?.Dispose();
        pictureBox.Image?.Dispose();
        base.OnFormClosing(e);
    }
}