using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace FF12RNGHelper.Forms
{
    partial class FormSpawn2
    {
        public class FormSpawnLoader : IFormLoader
        {
            public void SaveForm(Form form, string filePath)
            {
                FormSpawn2 formSpawn = form as FormSpawn2;
                XDocument doc =
                    new XDocument(
                        new XElement("FormSpawn",
                            new XElement("Platform", new XAttribute("Type", formSpawn.cbPlatform.SelectedItem.ToString())),
                            new XElement("Characters",
                                new XElement("One",
                                    new XAttribute("Level", formSpawn.tbLevel1.Text),
                                    new XAttribute("Magic", formSpawn.tbMagic1.Text),
                                    new XAttribute("Spell", formSpawn.ddlSpellPow1.SelectedItem.ToString()),
                                    new XAttribute("Serenity", formSpawn.cbSerenity1.Checked)
                                ),                            
                                new XElement("Two",
                                    new XAttribute("Level", formSpawn.tbLevel2.Text),
                                    new XAttribute("Magic", formSpawn.tbMagic2.Text),
                                    new XAttribute("Spell", formSpawn.ddlSpellPow2.SelectedItem.ToString()),
                                    new XAttribute("Serenity", formSpawn.cbSerenity2.Checked)
                                ),                            
                                new XElement("Three",
                                    new XAttribute("Level", formSpawn.tbLevel3.Text),
                                    new XAttribute("Magic", formSpawn.tbMagic3.Text),
                                    new XAttribute("Spell", formSpawn.ddlSpellPow3.SelectedItem.ToString()),
                                    new XAttribute("Serenity", formSpawn.cbSerenity3.Checked)
                                )
                            ),
                            new XElement("Monsters",
                                new XElement("One",
                                    new XAttribute("MinChance", formSpawn.tbMin1.Text),
                                    new XAttribute("MaxChance", formSpawn.tbMax1.Text),
                                    new XAttribute("RngPosition", formSpawn.tbRNG1.Text)
                                ),
                                new XElement("Two",
                                    new XAttribute("MinChance", formSpawn.tbMin2.Text),
                                    new XAttribute("MaxChance", formSpawn.tbMax2.Text),
                                    new XAttribute("RngPosition", formSpawn.tbRNG2.Text)
                                )
                            )
                        )
                    );
                doc.Save(filePath);
            }

            public void LoadForm(Form form, string filePath)
            {
                FormSpawn2 formSpawn = form as FormSpawn2;
                if (formSpawn == null)
                {
                    MessageBox.Show(FormConstants.FormError);
                    return;
                }

                XDocument doc = XDocument.Load(filePath);
                if (doc.Root == null || !doc.Root.Name.LocalName.Equals("FormSpawn"))
                {
                    MessageBox.Show(string.Format(FormConstants.FileError, "Spawn"));
                    return;
                }

                try
                {
                    formSpawn.cbPlatform.SelectedIndex =
                        FormConstants.PlatformToIndex[doc.Root.Element("Platform").Attribute("Type").Value];

                    XElement character1 = doc.Root.Element("Characters").Element("One");
                    formSpawn.tbLevel1.Text = character1.Attribute("Level").Value;
                    formSpawn.tbMagic1.Text = character1.Attribute("Magic").Value;
                    formSpawn.ddlSpellPow1.SelectedIndex =
                        FormConstants.NameToIndexMap[character1.Attribute("Spell").Value];
                    formSpawn.cbSerenity1.Checked = bool.Parse(character1.Attribute("Serenity").Value);

                    XElement character2 = doc.Root.Element("Characters").Element("Two");
                    formSpawn.tbLevel2.Text = character2.Attribute("Level").Value;
                    formSpawn.tbMagic2.Text = character2.Attribute("Magic").Value;
                    formSpawn.ddlSpellPow2.SelectedIndex =
                        FormConstants.NameToIndexMap[character2.Attribute("Spell").Value];
                    formSpawn.cbSerenity2.Checked = bool.Parse(character2.Attribute("Serenity").Value);

                    XElement character3 = doc.Root.Element("Characters").Element("Three");
                    formSpawn.tbLevel3.Text = character3.Attribute("Level").Value;
                    formSpawn.tbMagic3.Text = character3.Attribute("Magic").Value;
                    formSpawn.ddlSpellPow3.SelectedIndex =
                        FormConstants.NameToIndexMap[character3.Attribute("Spell").Value];
                    formSpawn.cbSerenity3.Checked = bool.Parse(character3.Attribute("Serenity").Value);

                    XElement chest1 = doc.Root.Element("Monsters").Element("One");
                    formSpawn.tbMin1.Text = chest1.Attribute("MinChance").Value;
                    formSpawn.tbMax1.Text = chest1.Attribute("MaxChance").Value;
                    formSpawn.tbRNG1.Text = chest1.Attribute("RngPosition").Value;

                    XElement chest2 = doc.Root.Element("Monsters").Element("Two");
                    formSpawn.tbMin2.Text = chest2.Attribute("MinChance").Value;
                    formSpawn.tbMax2.Text = chest2.Attribute("MaxChance").Value;
                    formSpawn.tbRNG2.Text = chest2.Attribute("RngPosition").Value;
                }
                catch (Exception)
                {
                    MessageBox.Show(FormConstants.MalformedError);
                }
            }
        }
    }
}