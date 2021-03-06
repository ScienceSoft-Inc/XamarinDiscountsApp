﻿using System;
using System.Collections.Generic;
using System.Linq;
using SCNDISC.Server.Domain.Aggregates.Partners;
using SCNDISC.Server.Core.Models.Partner;

namespace SCNDISC.Server.Core.Mapper
{
    public abstract class ContactMapper
    {
        public static ContactModel MapToContactModels(IEnumerable<LocalizableText> address, IEnumerable<Phone> phone, string coordinates, string Id)
        {
            ContactModel contact = new ContactModel();
            contact.Id = Id;
            if (address!=null && address.Count() > 0)
            {
                if (address.First(x => x.Lan == "EN").LocText != null)
                {
                    contact.Address_En = address.First(x => x.Lan == "EN").LocText;
                }

                if (address.FirstOrDefault(x => x.Lan == "RU")?.LocText != null)
                {
                    contact.Address_Ru = address.First(x => x.Lan == "RU").LocText;
                }
            }

            if (phone!= null && phone.Count() > 0)
            {
                if (phone.Count() == 1)
                {
                    contact.PhoneNumber1 = phone.ElementAt(0).Number;
                }

                if (phone.Count() == 2)
                {
                    contact.PhoneNumber1 = phone.ElementAt(0).Number;
                    contact.PhoneNumber2 = phone.ElementAt(1).Number;
                }
            }

            contact.Coordinates = coordinates;
            return contact;
        }

        public static IEnumerable<LocalizableText> MapFromContactModelToAddress(ContactModel contact)
        {
            List<LocalizableText> addresses = new List<LocalizableText>();
            LocalizableText addressRu = new LocalizableText
            {
                Lan = Languages.Ru,
                LocText = contact.Address_Ru ?? string.Empty
            };
            LocalizableText addressEn = new LocalizableText
            {
                Lan = Languages.En,
                LocText = contact.Address_En ?? string.Empty
            };
            addresses.Add(addressEn);
            addresses.Add(addressRu);
            return addresses;
        }
        public static IEnumerable<Phone> MapFromContactModelToPhone(ContactModel contact)
        {
            List<Phone> phones = new List<Phone>();
            Phone phone = new Phone();
            if (contact.PhoneNumber1 != null)
            {
                phone.Number = contact.PhoneNumber1;
                phones.Add(phone);
            }
            Phone phoneTwo = new Phone();
            if (contact.PhoneNumber2 != null)
            {
                phoneTwo.Number = contact.PhoneNumber2;
                phones.Add(phoneTwo);
            }

            return phones;
        }

        public static string MapFromContactModelToCoordinates(ContactModel contact)
        {
            return contact.Coordinates;
        }
    }
}