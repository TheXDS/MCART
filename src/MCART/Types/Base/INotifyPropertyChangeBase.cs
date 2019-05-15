using System.Collections.Generic;
using System.Reflection;

namespace TheXDS.MCART.Types.Base
{
    public interface INotifyPropertyChangeBase : IRefreshable
    {
        /// <summary>
        ///     Agrega un objeto al cual reenviar los eventos de cambio de
        ///     valor de propiedad.
        /// </summary>
        /// <param name="source">
        ///     Objeto a registrar para el reenvío de eventos de cambio de
        ///     valor de propiedad.
        /// </param>
        void ForwardChange(INotifyPropertyChangeBase source);

        /// <summary>
        ///     Notifica desde un punto externo el cambio en el valor de una propiedad.
        /// </summary>
        /// <param name="properties">
        ///     Colección con los nombres de las propiedades a notificar.
        /// </param>
        void Notify(IEnumerable<PropertyInfo> properties);

        /// <summary>
        ///     Notifica desde un punto externo el cambio en el valor de una propiedad.
        /// </summary>
        /// <param name="properties">
        ///     Colección con los nombres de las propiedades a notificar.
        /// </param>
        void Notify(IEnumerable<string> properties);

        /// <summary>
        ///     Notifica desde un punto externo el cambio en el valor de una propiedad.
        /// </summary>
        /// <param name="properties">
        ///     Colección con los nombres de las propiedades a notificar.
        /// </param>
        void Notify(params string[] properties);

        /// <summary>
        ///     Quita un objeto de la lista de reenvíos de eventos de cambio de
        ///     valor de propiedad.
        /// </summary>
        /// <param name="source">
        ///     Elemento a quitar de la lista de reenvío.
        /// </param>
        void RemoveForwardChange(INotifyPropertyChangeBase source);
    }
}