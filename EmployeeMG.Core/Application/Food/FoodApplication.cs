using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Food;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMG.Core.Application.Food
{
   public class FoodApplication:IFoodApplication
   {
       private readonly ApplicationContext _applicationContext;

       public FoodApplication(ApplicationContext applicationContext)
       {
           _applicationContext = applicationContext;
       }

       public async Task<ListFood> GetAll(int pageId, int take, int ffc, string ffn)
       {
           try
           {
               var result = _applicationContext.Food.Select(c => new FoodModel()
               {
                   Code = c.Code,
                   Name = c.FoodName
               });

               if (!string.IsNullOrWhiteSpace(ffn))
                   result = result.Where(c => c.Name.Contains(ffn));

               if (ffc != 0)
                   result = result.Where(c => c.Code == ffc);


               var skip = (pageId - 1) * take;

               var model = new ListFood()
               {
                   FilterFoodCode = ffc,
                   FilterFoodName = ffn,
                   FoodModels = await result.OrderByDescending(c => c.Name).Skip(skip).Take(take).ToListAsync()
               };

               model.GenaratPaging(result, pageId, take);
               return model;

            }
           catch (Exception)
           {
               return null;
           }
       }

       public async Task<OperationResult> Create(string foodName)
       {
           OperationResult operationResult = new OperationResult();

            Random r =new  Random();

            try
            {
                var food = new TFood()
                {
                    Code = r.Next(111,999),
                    FoodName = foodName
                };

                if (await _applicationContext.Food.AnyAsync(c => c.Code == food.Code))
                    return operationResult.Failed("مشکلی از سرور به وجود آمده لطفا دوباره امتحان کنید");

                if (await _applicationContext.Food.AnyAsync(c => c.FoodName == food.FoodName))
                    return operationResult.Failed("این غدا قبلا ثبت شده");

                await _applicationContext.Food.AddAsync(food);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
       }

       public async Task<OperationResult> Delete(int ID)
       {
           OperationResult operationResult = new OperationResult();
           try
           {
               var _food = await _applicationContext.Food.FindAsync(Convert.ToInt32(ID));
               _applicationContext.Food.Remove(_food);
               await _applicationContext.SaveChangesAsync();
               return operationResult.Succeeded();
           }
           catch (Exception)
           {
               return operationResult.Failed();
           }


       }

   }
}
