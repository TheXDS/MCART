using TheXDS.MCART.Annotations;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    ///     Define una serie de miembros a implementar por una clase que
    ///     implemente funcionalidades básicas de edición de entidades de
    ///     ViewModel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IViewModel<T>
    {
        /// <summary>
        ///     Edita la instancia de <typeparamref name="T"/> dentro de este
        ///     ViewModel.
        /// </summary>
        /// <param name="entity">
        ///     Entidad desde la cual extraer información.
        /// </param>
        void Edit([NotNull] T entity);
        /// <summary>
        ///     Instancia de la entidad controlada por este ViewModel.
        /// </summary>
        T Entity { get; }
    }
}