namespace ObserverPattern
{
    public interface Isubject
    {
        void ResisterObserver(IObserver opserver);
        void RemoveObserver(IObserver opserver);
        void NotifyObservers();
    }

    public interface IObserver
    {
        void UpdateData(bool isSelected);
    }
}
