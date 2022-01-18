using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Cost;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMG.Core.Application.Cost
{
   public class CostApplication:ICostApplication
   {
       private readonly ApplicationContext _applicationContext;

       public CostApplication(ApplicationContext applicationContext)
       {
           _applicationContext = applicationContext;
       }


       public async Task<ListCost> GetAll(int pageId, int take, string FilterCostDate, string FilterSort)
       {
           try
           {

               var result = _applicationContext.Cost.Select(c => new CostModel()
               {
                   Id = c.Id,
                   Amount = c.Amount,
                   CostDate = c.CostDate,
                   Forthe =c.Forthe,
               });

               if (!string.IsNullOrWhiteSpace(FilterCostDate))
                   result = result.Where(c => c.CostDate == FilterCostDate);

               if (FilterSort == "بیشترین هزینه")
                   result = result.OrderByDescending(c => c.Amount); 
               
               if (FilterSort == "کمترین هزینه")
                   result = result.OrderBy(c => c.Amount);
                


               var skip = (pageId - 1) * take;

               var model = new ListCost()
               {
                   FilterSort = FilterSort,
                   FilterDateCost = FilterCostDate,
                   CostModels = await result.Skip(skip).Take(take).ToListAsync(),
               };

               model.GenaratPaging(result, pageId, take);
               return model;
           }
           catch (Exception)
           {
               return null;
           }
       }

       public async Task<OperationResult> Create(string forthe, string dateCost, long amount)
       {
           OperationResult operationResult = new OperationResult();
           try
           {
               Random r = new Random();

                var _Cost = new TCost()
                {
                    Id = r.Next(111111,999999),
                    Forthe = forthe,
                    Amount = amount,
                    CostDate = dateCost.ToGeorgianDateTime().ToFarsi(),
                };

                await _applicationContext.Cost.AddAsync(_Cost);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

           }
           catch (Exception)
           {
               return operationResult.Failed();
           }
       }

       public async Task<CostModel> GetForEdit(int Id)
       {
           try
           {
               var _cost = await _applicationContext.Cost.Where(c=>c.Id == Id).Select(c => new CostModel()
               {
                   Id = c.Id,
                   Amount = c.Amount,
                   CostDate = c.CostDate.ToGeorgianDateTime().ToFileName(),
                   Forthe = c.Forthe

               }).SingleOrDefaultAsync();

               return _cost;

           }
           catch (Exception)
           {
               return null;
           }
       }

       public async Task<OperationResult> Edit(CostModel costModel)
       {
            OperationResult operationResult= new OperationResult();

           try
           {
               var _cost = await _applicationContext.Cost.FindAsync(costModel.Id);

               _cost.Amount = costModel.Amount;
               _cost.Forthe = costModel.Forthe;
               _cost.CostDate = costModel.CostDate.ToGeorgianDateTime().ToFarsi();

               _applicationContext.Cost.Update(_cost);
               await _applicationContext.SaveChangesAsync();
               return operationResult.Succeeded();

           }
           catch (Exception e)
           {
               return operationResult.Failed();
           }
       }

       public async Task<OperationResult> Delete(int ID)
       {
           OperationResult operationResult = new OperationResult();

           try
           {
               var _cost = await _applicationContext.Cost.FindAsync(Convert.ToInt64(ID));

               _applicationContext.Cost.Remove(_cost);
               await _applicationContext.SaveChangesAsync();
               return operationResult.Succeeded();

           }
           catch (Exception e)
           {
               return operationResult.Failed();
           }
       }
    }
}
