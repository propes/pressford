using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public interface IModelMapper<TModel, TViewModel>
    {
        TViewModel MapToViewModel(TModel model);

        void MapToViewModel(TModel model, TViewModel viewModel);

        TModel MapToModel(TViewModel viewModel);

        void MapToModel(TViewModel viewModel, TModel model);
    }
}
