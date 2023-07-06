using System.Reflection;

namespace Invoice.Services;

public interface IModelService
{
    T GlobalConfigureModel<T>(T model);
}

public class ModelService : IModelService
{
    public T GlobalConfigureModel<T>(T obj)
    {
        obj = SetAllDateTimePropertiesToUtcDateTimeKind(obj);

        return obj;
    }

    T SetAllDateTimePropertiesToUtcDateTimeKind<T>(T obj)
    {
        Type t = typeof(T);

        // Loop through the properties.
        PropertyInfo[] props = t.GetProperties();
        for (int i = 0; i < props.Length; i++)
        {
            PropertyInfo p = props[i];
            // If a property is DateTime or DateTime?, set DateTimeKind to DateTimeKind.Utc.
            if (p.PropertyType == typeof(DateTime))
            {
                DateTime date = (DateTime)p.GetValue(obj, null);
                date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
                p.SetValue(obj, date, null);
            }
            // Same check for nullable DateTime.
            else if (p.PropertyType == typeof(Nullable<DateTime>))
            {
                DateTime? date = (DateTime?)p.GetValue(obj, null);
                if (date.HasValue)
                {
                    DateTime? newDate = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);
                    p.SetValue(obj, newDate, null);
                }
            }
        }
        return obj;
    }
}
