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
    public class FoodApplication : IFoodApplication
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

        public async Task<List<FoodModel>> GetForCreateOffered()
        {
            var _foods = await _applicationContext.Food.Select(c => new FoodModel()
            {
                Code = c.Code,
                Name = c.FoodName,
            }).ToListAsync();
            return _foods;
        }


        public async Task<ListNUTritionCard> GetAllCard(int pageId, int take, int fpc)
        {
            try
            {
                var result = _applicationContext.NUTritionCard.Select(c => new NUTritionCardModel()
                {
                    Id = c.Id,
                    Balance = c.Balance,
                    CodePersonnel = c.PersonnelCode
                });

                if (fpc != 0)
                    result = result.Where(c => c.CodePersonnel == fpc);

                var skip = (pageId - 1) * take;

                var model = new ListNUTritionCard()
                {
                    CodePersonnel = fpc,
                    NuTritionCardModels = await result.OrderByDescending(c => c.Balance).Skip(skip).Take(take).ToListAsync()
                };

                model.GenaratPaging(result, pageId, take);
                return model;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> CreateNUTCard(int codePersonnley, int Balabce)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                if (!await _applicationContext.Employee.AnyAsync(c => c.PersonnelCode == codePersonnley))
                    return operationResult.Failed("کد پرسنلی اشتباه است");

                Random r = new Random();
                var _NutCard = new TNUTritionCard()
                {
                    Id = r.Next(11111, 999999),
                    PersonnelCode = codePersonnley,
                    Balance = Balabce,
                };

                if (await _applicationContext.NUTritionCard.AnyAsync(c =>
                    c.PersonnelCode == codePersonnley))
                    return operationResult.Failed("برای این کارمند قبلا کارت تغذیه ثبت شده");

                await _applicationContext.NUTritionCard.AddAsync(_NutCard);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<NUTritionCardModel> GeForbalanceAdd(int ID)
        {
            try
            {
                var _UntCard = await _applicationContext.NUTritionCard.Where(c => c.Id == ID).Select(c =>
                    new NUTritionCardModel()
                    {
                        CodePersonnel = c.PersonnelCode,
                        Balance = c.Balance,
                        Id = c.Id
                    }).SingleOrDefaultAsync();

                return _UntCard;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> balanceAdd(NUTritionCardModel nuTritionCardModel)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _card = await _applicationContext.NUTritionCard.FindAsync(nuTritionCardModel.Id);

                _card.Balance += nuTritionCardModel.Balance;

                _applicationContext.Update(_card);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> DeleteNUTritionCard(int ID)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _card = await _applicationContext.NUTritionCard.FindAsync(ID);

                _applicationContext.Remove(_card);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<OperationResult> Create(string foodName)
        {
            OperationResult operationResult = new OperationResult();

            Random r = new Random();

            try
            {
                var food = new TFood()
                {
                    Code = r.Next(111, 999),
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

        public async Task<ListFoodOffered> GetAllFoodOffered(int pageId, int take, string FilterDateOffered,string FilterMeal)
        {
            try
            {

                var result = _applicationContext.FoodOffered.Select(c => new FoodOfferedModel()
                {
                    ID = c.Id,
                    FoodName = c.Food,
                    Day = c.Day,
                    Meal = c.Meal,
                    DateOffered = c.OfferedDate,

                });


                if (!string.IsNullOrWhiteSpace(FilterDateOffered))
                {
                    var FilDate = FilterDateOffered.ToGeorgianDateTime().ToFarsi();

                    result = result.Where(c => c.DateOffered == FilDate);
                }

                if (!string.IsNullOrWhiteSpace(FilterMeal))
                    result = result.Where(c => c.Meal.Contains(FilterMeal));

                var skip = (pageId - 1) * take;

                var model = new ListFoodOffered()
                {
                    FilterDateOffered = FilterDateOffered,
                    FoodOfferedModels = await result.OrderByDescending(c => c.ID).Skip(skip).Take(take).ToListAsync()
                };

                model.GenaratPaging(result, pageId, take);
                return model;

            }
            catch (Exception)
            {
                return null;
            }
        }



        public async Task<string> ShowDay(string date)
        {
            var DayOfWeek = date.ToGeorgianDateTime().DayOfWeek;

            var _Day = "";

            switch (DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return _Day = "شنبه";

                case DayOfWeek.Sunday:
                    return _Day = "یکشنبه";

                case DayOfWeek.Monday:
                    return _Day = "دوشنبه";

                case DayOfWeek.Tuesday:
                    return _Day = "سشنبه";

                case DayOfWeek.Wednesday:
                    return _Day = "چهارشنبه";

                case DayOfWeek.Thursday:
                    return _Day = "پنجشنبه";

                case DayOfWeek.Friday:
                    return _Day = "جمعه";

                default:
                    throw new Exception();
            }
        }

        public async Task<OperationResult> CreateOffered(FoodOfferedModel foodOfferedModel)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _Day = foodOfferedModel.Day;

                Random r = new Random();
                var _FoodOffered = new TFoodOffered()
                {
                    Id = r.Next(11111, 99999),
                    Day = _Day,
                    Food = foodOfferedModel.FoodName,
                    Meal = foodOfferedModel.Meal,
                    OfferedDate = foodOfferedModel.DateOffered.ToGeorgianDateTime().ToFarsi()
                };

                if (await _applicationContext.FoodOffered.AnyAsync(c =>
                    c.Meal == _FoodOffered.Meal && c.OfferedDate == _FoodOffered.OfferedDate))
                    return operationResult.Failed($"وعده {_FoodOffered.Meal} در تاریخ {_FoodOffered.OfferedDate} ارائه  شده است");

                await _applicationContext.FoodOffered.AddAsync(_FoodOffered);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<FoodOfferedModel> GetForEditFoodOffered(int ID)
        {
            try
            {
                var _FoodOffered = await _applicationContext.FoodOffered.Where(c => c.Id == ID).Select(c => new FoodOfferedModel()
                {
                    ID = c.Id,
                    Meal = c.Meal,
                    FoodName = c.Food,
                    Day = c.Day,
                    DateOffered = c.OfferedDate.ToGeorgianDateTime().ToFarsi()

                }).SingleOrDefaultAsync();

                var e1 = Convert.ToInt32(_FoodOffered.DateOffered.Split("/")[0]).ToPersianNumber();
                var e2 = Convert.ToInt32(_FoodOffered.DateOffered.Split("/")[1]).ToPersianNumber();
                var e3 = Convert.ToInt32(_FoodOffered.DateOffered.Split("/")[2]).ToPersianNumber();

                var FarsiDate = e1 +"/"+e2 +"/"+e3;

                _FoodOffered.DateOffered = FarsiDate;

                return _FoodOffered;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> Edit(FoodOfferedModel foodOfferedModel)
        {
            OperationResult operationResult = new OperationResult();
            
            try
            {
                var _foodOff = await _applicationContext.FoodOffered.FindAsync(foodOfferedModel.ID);

                _foodOff.OfferedDate = foodOfferedModel.DateOffered.ToGeorgianDateTime().ToFarsi();
                _foodOff.Meal = foodOfferedModel.Meal;
                _foodOff.Day = foodOfferedModel.Day;
                _foodOff.Food = foodOfferedModel.FoodName;

                if(await _applicationContext.FoodOffered.AnyAsync(c=>c.Meal==_foodOff.Meal && c.OfferedDate==_foodOff.OfferedDate && c.Id != _foodOff.Id))
                    return operationResult.Failed($"وعده {_foodOff.Meal} در تاریخ {_foodOff.OfferedDate} ارائه  شده است");

                _applicationContext.FoodOffered.Update(_foodOff);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();
            }
            catch
            {
                return operationResult.Failed();
            }
        }

        public async Task<OperationResult> DeleteFoodOffered(int ID)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _foodOff = await _applicationContext.FoodOffered.FindAsync(ID);

                _applicationContext.FoodOffered.Remove(_foodOff);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();
            }
            catch
            {
                return operationResult.Failed();
            }
        }

        public async Task<OperationResult> Reservefood(int Codep, int ID)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                if (!await _applicationContext.Employee.AnyAsync(c => c.PersonnelCode == Codep))
                    return operationResult.Failed("کد پرسنلی اشتباه است");

                if(await _applicationContext.NUTritionCard.AnyAsync(c=>c.PersonnelCode == Codep && c.Balance < 20000))
                    return operationResult.Failed("موجودی کارت تغذیه کمتر از 20,000 تومان است");
                
                Random r =new Random();
                var ReservationFood = new TReservationFood()
                {
                    Id = r.Next(111111,999999),
                    OfferedCode = ID,
                    PersonnelCode = Codep
                };

                if (await _applicationContext.ReservationFood.AnyAsync(c =>
                    c.OfferedCode == ReservationFood.OfferedCode && c.PersonnelCode == ReservationFood.PersonnelCode))
                    return operationResult.Failed("این وعده برای این کارمند قبلا رزرو شده است");

                await _applicationContext.ReservationFood.AddAsync(ReservationFood);

                var _TNUCard = await _applicationContext.NUTritionCard.Where(c=>c.PersonnelCode == ReservationFood.PersonnelCode).SingleOrDefaultAsync();
                _TNUCard.Balance -= 20000;
                _applicationContext.Update(_TNUCard);

                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded("این وعده برای کارمند رزرو شد");
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<List<int>> ReservationFoodEmployeeShow(int ID)
        {
            var _data = await _applicationContext.ReservationFood.Where(c => c.OfferedCode == ID)
                .Select(c => c.PersonnelCode).ToListAsync();

            return _data;
        }
    }
}
