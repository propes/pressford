using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public abstract class ModelMapper<TModel, TViewModel> : IModelMapper<TModel, TViewModel>
        where TModel : new()
        where TViewModel : new()
    {
        public virtual TViewModel MapToViewModel(TModel model)
        {
            var viewModel = new TViewModel();
            MapToViewModel(model, viewModel);

            return viewModel;
        }

        public abstract void MapToViewModel(TModel model, TViewModel viewModel);

        public virtual TModel MapToModel(TViewModel viewModel)
        {
            var model = new TModel();
            MapToModel(viewModel, model);

            return model;
        }

        public abstract void MapToModel(TViewModel viewModel, TModel model);
    }
}
