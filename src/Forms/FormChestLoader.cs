using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace FF12RNGHelper.Forms
{
    partial class FormChest2
    {
        public class FormChestLoader : IFormLoader
        {
            public void SaveForm(Form form, string filePath)
            {
                FormChest2 formChest = form as FormChest2;
                XDocument doc =
                    new XDocument(
                        new XElement("FormChest",
                            new XElement("Platform",
                                new XAttribute("Type", formChest.cbPlatform.SelectedItem.ToString())),
                            new XElement("Characters",
                                new XElement("One",
                                    new XAttribute("Level", formChest.tbLevel1.Text),
                                    new XAttribute("Magic", formChest.tbMagic1.Text),
                                    new XAttribute("Spell", formChest.ddlSpellPow1.SelectedItem.ToString()),
                                    new XAttribute("Serenity", formChest.cbSerenity1.Checked)
                                ),
                                new XElement("Two",
                                    new XAttribute("Level", formChest.tbLevel2.Text),
                                    new XAttribute("Magic", formChest.tbMagic2.Text),
                                    new XAttribute("Spell", formChest.ddlSpellPow2.SelectedItem.ToString()),
                                    new XAttribute("Serenity", formChest.cbSerenity2.Checked)
                                ),
                                new XElement("Three",
                                    new XAttribute("Level", formChest.tbLevel3.Text),
                                    new XAttribute("Magic", formChest.tbMagic3.Text),
                                    new XAttribute("Spell", formChest.ddlSpellPow3.SelectedItem.ToString()),
                                    new XAttribute("Serenity", formChest.cbSerenity3.Checked)
                                )
                            ),
                            new XElement("Chests",
                                new XElement("One",
                                    new XAttribute("AppearsChance", formChest.textBox1.Text),
                                    new XAttribute("RngPosition", formChest.textBox2.Text),
                                    new XAttribute("GilChance", formChest.textBox3.Text),
                                    new XAttribute("Item1Chance", formChest.textBox4.Text),
                                    new XAttribute("GilAmount", formChest.textBox5.Text),
                                    new XAttribute("WantItem1", formChest.cbWantItem1First.Checked)
                                ),
                                new XElement("Two",
                                    new XAttribute("AppearsChance", formChest.textBox10.Text),
                                    new XAttribute("RngPosition", formChest.textBox9.Text),
                                    new XAttribute("GilChance", formChest.textBox8.Text),
                                    new XAttribute("Item1Chance", formChest.textBox7.Text),
                                    new XAttribute("GilAmount", formChest.textBox6.Text),
                                    new XAttribute("WantItem1", formChest.cbWantItem1Second.Checked)
                                )
                            )
                        )
                    );
                doc.Save(filePath);
            }

            public void LoadForm(Form form, string filePath)
            {
                FormChest2 formChest = form as FormChest2;
                if (formChest == null)
                {
                    MessageBox.Show(FormConstants.FormError);
                    return;
                }

                XDocument doc = XDocument.Load(filePath);
                if (doc.Root == null || !doc.Root.Name.LocalName.Equals("FormChest"))
                {
                    MessageBox.Show(string.Format(FormConstants.FileError, "Chest"));
                    return;
                }

                try
                {
                    formChest.cbPlatform.SelectedIndex =
                        FormConstants.PlatformToIndex[doc.Root.Element("Platform").Attribute("Type").Value];

                    XElement character1 = doc.Root.Element("Characters").Element("One");
                    formChest.tbLevel1.Text = character1.Attribute("Level").Value;
                    formChest.tbMagic1.Text = character1.Attribute("Magic").Value;
                    formChest.ddlSpellPow1.SelectedIndex =
                        FormConstants.NameToIndexMap[character1.Attribute("Spell").Value];
                    formChest.cbSerenity1.Checked = bool.Parse(character1.Attribute("Serenity").Value);

                    XElement character2 = doc.Root.Element("Characters").Element("Two");
                    formChest.tbLevel2.Text = character2.Attribute("Level").Value;
                    formChest.tbMagic2.Text = character2.Attribute("Magic").Value;
                    formChest.ddlSpellPow2.SelectedIndex =
                        FormConstants.NameToIndexMap[character2.Attribute("Spell").Value];
                    formChest.cbSerenity2.Checked = bool.Parse(character2.Attribute("Serenity").Value);

                    XElement character3 = doc.Root.Element("Characters").Element("Three");
                    formChest.tbLevel3.Text = character3.Attribute("Level").Value;
                    formChest.tbMagic3.Text = character3.Attribute("Magic").Value;
                    formChest.ddlSpellPow3.SelectedIndex =
                        FormConstants.NameToIndexMap[character3.Attribute("Spell").Value];
                    formChest.cbSerenity3.Checked = bool.Parse(character3.Attribute("Serenity").Value);

                    XElement chest1 = doc.Root.Element("Chests").Element("One");
                    formChest.textBox1.Text = chest1.Attribute("AppearsChance").Value;
                    formChest.textBox2.Text = chest1.Attribute("RngPosition").Value;
                    formChest.textBox3.Text = chest1.Attribute("GilChance").Value;
                    formChest.textBox4.Text = chest1.Attribute("Item1Chance").Value;
                    formChest.textBox5.Text = chest1.Attribute("GilAmount").Value;
                    formChest.cbWantItem1First.Checked = bool.Parse(chest1.Attribute("WantItem1").Value);

                    XElement chest2 = doc.Root.Element("Chests").Element("Two");
                    formChest.textBox10.Text = chest2.Attribute("AppearsChance").Value;
                    formChest.textBox9.Text = chest2.Attribute("RngPosition").Value;
                    formChest.textBox8.Text = chest2.Attribute("GilChance").Value;
                    formChest.textBox7.Text = chest2.Attribute("Item1Chance").Value;
                    formChest.textBox6.Text = chest2.Attribute("GilAmount").Value;
                    formChest.cbWantItem1Second.Checked = bool.Parse(chest2.Attribute("WantItem1").Value);
                }
                catch (Exception)
                {
                    MessageBox.Show(FormConstants.MalformedError);
                }
            }
        }
    }
}