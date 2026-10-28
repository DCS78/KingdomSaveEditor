using KHSave.LibPersona3;
using KHSave.LibPersona3.Types;
using System.IO;
using Xunit;

namespace KHSave.Tests
{
    public class Persona3Tests
    {
        [Theory]
        [InlineData("Saves/BASLUS-21569Save2")]
        [InlineData("Saves/P3PSAVE.BIN")]
        public void IsValid(string fileName) => File.OpenRead(fileName)
            .Using(stream => Assert.True(SavePersona3.IsValid(stream.SetPosition(1))));

        [Theory]
        [InlineData("Saves/BASLUS-21569Save2")]
        [InlineData("Saves/P3PSAVE.BIN")]
        public void WriteBackTheSameExactFile(string fileName) =>
            File.OpenRead(fileName).Using(stream => Helpers.AssertStream(stream, inStream =>
            {
                var save = SavePersona3.Read(inStream);

                var outStream = new MemoryStream();
                save.Write(outStream);

                return outStream;
            }));

        [Fact]
        public void ReadPersona3Vanilla()
        {
            var save = File.OpenRead("Saves/BASLUS-21569Save2").Using(SavePersona3.Read);
            Assert.Equal(GameVersion.Vanilla, save.Version);

            Assert.False(save.IsFemaleProtagonist);
            Assert.Equal(Characters.Yukari, save.BattlePartyMember1);
            Assert.Equal(Characters.Ken, save.BattlePartyMember2);
            Assert.Equal(Characters.Mitsuru, save.BattlePartyMember3);
            Assert.Equal(1766954, save.Money);

            Assert.Equal(Demons.Dominion, save.Persona[0].Id);
            Assert.Equal(Demons.Titania, save.Persona[1].Id);
            Assert.Equal(Demons.Succubus, save.Persona[2].Id);
            Assert.Equal(Demons.Koumokuten, save.Persona[9].Id);

            Assert.Equal(55, save.Persona[0].Level);
            Assert.Equal(33, save.Persona[0].Strength);
            Assert.Equal(49, save.Persona[0].Magic);
            Assert.Equal(29, save.Persona[0].Endurance);
            Assert.Equal(34, save.Persona[0].Agility);
            Assert.Equal(37, save.Persona[0].Luck);
            Assert.Equal(Skill.Hamaon, save.Persona[0].Skills[0]);
            Assert.Equal(Skill.ElecBoost, save.Persona[0].Skills[1]);
            Assert.Equal(Skill.NullShock, save.Persona[0].Skills[2]);
            Assert.Equal(Skill.Zionga, save.Persona[0].Skills[3]);
            Assert.Equal(Skill.EndureDark, save.Persona[0].Skills[4]);
            Assert.Equal(Skill.HamaBoost, save.Persona[0].Skills[5]);
            Assert.Equal(Skill.Mazionga, save.Persona[0].Skills[6]);
            Assert.Equal(Skill.ResistDark, save.Persona[0].Skills[7]);

            Assert.Equal(0, save.ExpendableItems[(int)ExpendableItems.Dummy]);
            Assert.Equal(99, save.ExpendableItems[(int)ExpendableItems.Medicine]);
            Assert.Equal(99, save.ExpendableItems[(int)ExpendableItems.MedicalPowder]);
            Assert.Equal(11, save.ExpendableItems[(int)ExpendableItems.Bead]);
            Assert.Equal(5, save.ExpendableItems[(int)ExpendableItems.SnuffSoul]);
            Assert.Equal(10, save.ExpendableItems[(int)ExpendableItems.MazioGem]);
            Assert.Equal(3, save.ExpendableItems[(int)ExpendableItems.MegidoGem]);
        }

        [Fact]
        public void ReadPersona3Portable()
        {
            var save = File.OpenRead("Saves/P3PSAVE.BIN").Using(SavePersona3.Read);
            Assert.Equal(GameVersion.Portable, save.Version);

            Assert.True(save.IsFemaleProtagonist);
            Assert.Equal(Characters.Akihiko, save.BattlePartyMember1);
            Assert.Equal(Characters.Yukari, save.BattlePartyMember2);
            Assert.Equal(Characters.Empty, save.BattlePartyMember3);
            Assert.Equal(Characters.Empty, save.BattlePartyMember4);
            Assert.Equal(34624, save.Money);

            Assert.Equal(12, save.Persona.Length);
            Assert.Equal(Demons.Chimera, save.Persona[0].Id);
            Assert.Equal(11, save.Persona[0].Level);
            Assert.Equal(11, save.Persona[0].Strength);
            Assert.Equal(6, save.Persona[0].Magic);
            Assert.Equal(10, save.Persona[0].Endurance);
            Assert.Equal(8, save.Persona[0].Agility);
            Assert.Equal(5, save.Persona[0].Luck);
            Assert.Equal(Skill.Cleave, save.Persona[0].Skills[0]);
            Assert.Equal(Skill.Agi, save.Persona[0].Skills[1]);
            Assert.Equal(Skill.Bewilder, save.Persona[0].Skills[2]);
            Assert.Equal(Skill.Bash, save.Persona[0].Skills[3]);
            Assert.Equal(Skill.Patra, save.Persona[0].Skills[4]);
            Assert.Equal(Skill.AutoSukukaja, save.Persona[0].Skills[5]);
            Assert.Equal(Skill.SonicPunch, save.Persona[0].Skills[6]);
            Assert.Equal(Skill.Empty, save.Persona[0].Skills[7]);

            Assert.Equal(0, save.ExpendableItems[(int)ExpendableItems.Dummy]);
            Assert.Equal(5, save.ExpendableItems[(int)ExpendableItems.Medicine]);
            Assert.Equal(0, save.ExpendableItems[(int)ExpendableItems.MedicalPowder]);
            Assert.Equal(0, save.ExpendableItems[(int)ExpendableItems.Bead]);
            Assert.Equal(0, save.ExpendableItems[(int)ExpendableItems.SnuffSoul]);
        }
    }
}
