using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FF12RNGHelper.Forms
{
    partial class FormSteal2
    {
        public class FormStealLoader : IFormLoader
        {
            public void SaveForm(Form form, string filePath)
            {
                FormSteal2 formSteal = form as FormSteal2;
                XDocument doc =
                    new XDocument(
                        new XElement("FormSteal",
                            new XElement("Platform",
                                new XAttribute("Type", formSteal.cbPlatform.SelectedItem.ToString())),
                            new XElement("Characters",
                                new XElement("One",
                                    new XAttribute("Level", formSteal.tbLevel1.Text),
                                    new XAttribute("Magic", formSteal.tbMagic1.Text),
                                    new XAttribute("Spell", formSteal.ddlSpellPow1.SelectedItem.ToString()),
                                    new XAttribute("Serenity", formSteal.cbSerenity1.Checked)
                                ),
                                new XElement("Two",
                                    new XAttribute("Level", formSteal.tbLevel2.Text),
                                    new XAttribute("Magic", formSteal.tbMagic2.Text),
                                    new XAttribute("Spell", formSteal.ddlSpellPow2.SelectedItem.ToString()),
                                    new XAttribute("Serenity", formSteal.cbSerenity2.Checked)
                                ),
                                new XElement("Three",
                                    new XAttribute("Level", formSteal.tbLevel3.Text),
                                    new XAttribute("Magic", formSteal.tbMagic3.Text),
                                    new XAttribute("Spell", formSteal.ddlSpellPow3.SelectedItem.ToString()),
                                    new XAttribute("Serenity", formSteal.cbSerenity3.Checked)
                                )
                            )
                        )
                    );
                doc.Save(filePath);
            }

            public void LoadForm(Form form, string filePath)
            {
                FormSteal2 formSteal = form as FormSteal2;
                if (formSteal == null)
                {
                    MessageBox.Show(FormConstants.FormError);
                    return;
                }

                XDocument doc = XDocument.Load(filePath);
                if (doc.Root == null || !doc.Root.Name.LocalName.Equals("FormSteal"))
                {
                    MessageBox.Show(string.Format(FormConstants.FileError, "Steal"));
                    return;
                }

                try
                {
                    formSteal.cbPlatform.SelectedIndex =
                        FormConstants.PlatformToIndex[doc.Root.Element("Platform").Attribute("Type").Value];

                    XElement character1 = doc.Root.Element("Characters").Element("One");
                    formSteal.tbLevel1.Text = character1.Attribute("Level").Value;
                    formSteal.tbMagic1.Text = character1.Attribute("Magic").Value;
                    formSteal.ddlSpellPow1.SelectedIndex =
                        FormConstants.NameToIndexMap[character1.Attribute("Spell").Value];
                    formSteal.cbSerenity1.Checked = bool.Parse(character1.Attribute("Serenity").Value);

                    XElement character2 = doc.Root.Element("Characters").Element("Two");
                    formSteal.tbLevel2.Text = character2.Attribute("Level").Value;
                    formSteal.tbMagic2.Text = character2.Attribute("Magic").Value;
                    formSteal.ddlSpellPow2.SelectedIndex =
                        FormConstants.NameToIndexMap[character2.Attribute("Spell").Value];
                    formSteal.cbSerenity2.Checked = bool.Parse(character2.Attribute("Serenity").Value);

                    XElement character3 = doc.Root.Element("Characters").Element("Three");
                    formSteal.tbLevel3.Text = character3.Attribute("Level").Value;
                    formSteal.tbMagic3.Text = character3.Attribute("Magic").Value;
                    formSteal.ddlSpellPow3.SelectedIndex =
                        FormConstants.NameToIndexMap[character3.Attribute("Spell").Value];
                    formSteal.cbSerenity3.Checked = bool.Parse(character3.Attribute("Serenity").Value);
                }
                catch (Exception)
                {
                    MessageBox.Show(FormConstants.MalformedError);
                }
            }
        }
    }
}