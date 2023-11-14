using System.Threading.Tasks;

public interface ISaveLoad
{
    public void SaveData(SaveData saveData);
    public Task LoadData();
}
