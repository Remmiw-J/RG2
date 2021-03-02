//////////////////////////////////
//Auto-generated. Do NOT modify!//
//////////////////////////////////

import { MessageKey, QueryKey, Type, EnumType, registerSymbol } from '../../../Framework/Signum.React/Scripts/Reflection'
import * as Entities from '../../../Framework/Signum.React/Scripts/Signum.Entities'
import * as Mailing from '../../../Extensions/Signum.React.Extensions/Mailing/Signum.Entities.Mailing'
import * as Authorization from '../../../Extensions/Signum.React.Extensions/Authorization/Signum.Entities.Authorization'
import * as Workflow from '../../../Extensions/Signum.React.Extensions/Workflow/Signum.Entities.Workflow'
import * as Files from '../../../Extensions/Signum.React.Extensions/Files/Signum.Entities.Files'
import * as Basics from '../../../Extensions/Signum.React.Extensions/Basics/Signum.Entities.Basics'
import * as Processes from '../../../Extensions/Signum.React.Extensions/Processes/Signum.Entities.Processes'
import * as Scheduler from '../../../Extensions/Signum.React.Extensions/Scheduler/Signum.Entities.Scheduler'



export const AdditionalInformationEmbedded = new Type<AdditionalInformationEmbedded>("AdditionalInformationEmbedded");
export interface AdditionalInformationEmbedded extends Entities.EmbeddedEntity {
  Type: "AdditionalInformationEmbedded";
  key: string;
  value: string;
}

export const AddressEmbedded = new Type<AddressEmbedded>("AddressEmbedded");
export interface AddressEmbedded extends Entities.EmbeddedEntity {
  Type: "AddressEmbedded";
  address: string;
  city: string;
  region: string | null;
  postalCode: string | null;
  country: string;
}

export const AllowLogin = new EnumType<AllowLogin>("AllowLogin");
export type AllowLogin =
  "WindowsAndWeb" |
  "WindowsOnly" |
  "WebOnly";

export const ApplicationConfigurationEntity = new Type<ApplicationConfigurationEntity>("ApplicationConfiguration");
export interface ApplicationConfigurationEntity extends Entities.Entity {
  Type: "ApplicationConfiguration";
  environment: string;
  databaseName: string;
  email: Mailing.EmailConfigurationEmbedded;
  emailSender: Mailing.EmailSenderConfigurationEntity;
  authTokens: Authorization.AuthTokenConfigurationEmbedded;
  webAuthn: Authorization.WebAuthnConfigurationEmbedded;
  workflow: Workflow.WorkflowConfigurationEmbedded;
  folders: FoldersConfigurationEmbedded;
  translation: TranslationConfigurationEmbedded;
}

export module ApplicationConfigurationOperation {
  export const Save : Entities.ExecuteSymbol<ApplicationConfigurationEntity> = registerSymbol("Operation", "ApplicationConfigurationOperation.Save");
}

export module BigStringFileType {
  export const Exceptions : Files.FileTypeSymbol = registerSymbol("FileType", "BigStringFileType.Exceptions");
  export const OperationLog : Files.FileTypeSymbol = registerSymbol("FileType", "BigStringFileType.OperationLog");
  export const ViewLog : Files.FileTypeSymbol = registerSymbol("FileType", "BigStringFileType.ViewLog");
  export const EmailMessage : Files.FileTypeSymbol = registerSymbol("FileType", "BigStringFileType.EmailMessage");
}

export module CatalogMessage {
  export const ProductName = new MessageKey("CatalogMessage", "ProductName");
  export const UnitPrice = new MessageKey("CatalogMessage", "UnitPrice");
  export const QuantityPerUnit = new MessageKey("CatalogMessage", "QuantityPerUnit");
  export const UnitsInStock = new MessageKey("CatalogMessage", "UnitsInStock");
}

export const CategoryEntity = new Type<CategoryEntity>("Category");
export interface CategoryEntity extends Entities.Entity {
  Type: "Category";
  categoryName: string;
  description: string;
  picture: Files.FileEmbedded | null;
}

export module CategoryOperation {
  export const Save : Entities.ExecuteSymbol<CategoryEntity> = registerSymbol("Operation", "CategoryOperation.Save");
}

export const Character = new Type<Character>("Character");
export interface Character extends Entities.Entity {
  Type: "Character";
  name: string;
  race: Race;
  class: Entities.Lite<Class>;
  spec: Entities.Lite<Spec>;
}

export module CharacterOperation {
  export const Save : Entities.ExecuteSymbol<Character> = registerSymbol("Operation", "CharacterOperation.Save");
}

export const Class = new Type<Class>("Class");
export interface Class extends Entities.Entity {
  Type: "Class";
  name: string;
  character: Entities.Lite<Character>;
}

export module ClassOperation {
  export const Save : Entities.ExecuteSymbol<Class> = registerSymbol("Operation", "ClassOperation.Save");
}

export const CompanyEntity = new Type<CompanyEntity>("Company");
export interface CompanyEntity extends CustomerEntity {
  Type: "Company";
  companyName: string;
  contactName: string;
  contactTitle: string;
}

export interface CustomerEntity extends Entities.Entity {
  address: AddressEmbedded;
  phone: string;
  fax: string | null;
}

export module CustomerOperation {
  export const Save : Entities.ExecuteSymbol<CustomerEntity> = registerSymbol("Operation", "CustomerOperation.Save");
}

export module CustomerQuery {
  export const Customer = new QueryKey("CustomerQuery", "Customer");
}

export const EmployeeEntity = new Type<EmployeeEntity>("Employee");
export interface EmployeeEntity extends Entities.Entity {
  Type: "Employee";
  lastName: string;
  firstName: string;
  title: string | null;
  titleOfCourtesy: string | null;
  birthDate: string | null;
  hireDate: string | null;
  address: AddressEmbedded;
  homePhone: string | null;
  extension: string | null;
  photo: Entities.Lite<Files.FileEntity> | null;
  notes: string | null;
  reportsTo: Entities.Lite<EmployeeEntity> | null;
  photoPath: string | null;
  territories: Entities.MList<TerritoryEntity>;
}

export module EmployeeOperation {
  export const Save : Entities.ExecuteSymbol<EmployeeEntity> = registerSymbol("Operation", "EmployeeOperation.Save");
}

export module EmployeeQuery {
  export const EmployeesByTerritory = new QueryKey("EmployeeQuery", "EmployeesByTerritory");
}

export const FoldersConfigurationEmbedded = new Type<FoldersConfigurationEmbedded>("FoldersConfigurationEmbedded");
export interface FoldersConfigurationEmbedded extends Entities.EmbeddedEntity {
  Type: "FoldersConfigurationEmbedded";
  exceptionsFolder: string;
  operationLogFolder: string;
  viewLogFolder: string;
  emailMessageFolder: string;
}

export const Item = new Type<Item>("Item");
export interface Item extends Entities.Entity {
  Type: "Item";
  name: string;
  itemId: number;
  fromRaid: Entities.Lite<Raid>;
}

export module ItemOperation {
  export const Save : Entities.ExecuteSymbol<Item> = registerSymbol("Operation", "ItemOperation.Save");
}

export const OrderDetailEmbedded = new Type<OrderDetailEmbedded>("OrderDetailEmbedded");
export interface OrderDetailEmbedded extends Entities.EmbeddedEntity {
  Type: "OrderDetailEmbedded";
  product: Entities.Lite<ProductEntity>;
  unitPrice: number;
  quantity: number;
  discount: number;
}

export const OrderDetailMixin = new Type<OrderDetailMixin>("OrderDetailMixin");
export interface OrderDetailMixin extends Entities.MixinEntity {
  Type: "OrderDetailMixin";
  discountCode: string | null;
}

export const OrderEntity = new Type<OrderEntity>("Order");
export interface OrderEntity extends Entities.Entity {
  Type: "Order";
  customer: CustomerEntity;
  employee: Entities.Lite<EmployeeEntity>;
  orderDate: string;
  requiredDate: string;
  shippedDate: string | null;
  cancelationDate: string | null;
  shipVia: Entities.Lite<ShipperEntity> | null;
  shipName: string | null;
  shipAddress: AddressEmbedded;
  freight: number;
  details: Entities.MList<OrderDetailEmbedded>;
  isLegacy: boolean;
  state: OrderState;
}

export const OrderFilterModel = new Type<OrderFilterModel>("OrderFilterModel");
export interface OrderFilterModel extends Entities.ModelEntity {
  Type: "OrderFilterModel";
  customer: Entities.Lite<CustomerEntity> | null;
  employee: Entities.Lite<EmployeeEntity> | null;
  minOrderDate: string | null;
  maxOrderDate: string | null;
}

export module OrderMessage {
  export const DiscountShouldBeMultpleOf5 = new MessageKey("OrderMessage", "DiscountShouldBeMultpleOf5");
  export const CancelShippedOrder0 = new MessageKey("OrderMessage", "CancelShippedOrder0");
  export const SelectAShipper = new MessageKey("OrderMessage", "SelectAShipper");
  export const SubTotalPrice = new MessageKey("OrderMessage", "SubTotalPrice");
}

export module OrderOperation {
  export const Create : Entities.ConstructSymbol_Simple<OrderEntity> = registerSymbol("Operation", "OrderOperation.Create");
  export const Save : Entities.ExecuteSymbol<OrderEntity> = registerSymbol("Operation", "OrderOperation.Save");
  export const Ship : Entities.ExecuteSymbol<OrderEntity> = registerSymbol("Operation", "OrderOperation.Ship");
  export const Cancel : Entities.ExecuteSymbol<OrderEntity> = registerSymbol("Operation", "OrderOperation.Cancel");
  export const CreateOrderFromCustomer : Entities.ConstructSymbol_From<OrderEntity, CustomerEntity> = registerSymbol("Operation", "OrderOperation.CreateOrderFromCustomer");
  export const CreateOrderFromProducts : Entities.ConstructSymbol_FromMany<OrderEntity, ProductEntity> = registerSymbol("Operation", "OrderOperation.CreateOrderFromProducts");
  export const Delete : Entities.DeleteSymbol<OrderEntity> = registerSymbol("Operation", "OrderOperation.Delete");
  export const CancelWithProcess : Entities.ConstructSymbol_FromMany<Processes.ProcessEntity, OrderEntity> = registerSymbol("Operation", "OrderOperation.CancelWithProcess");
}

export module OrderProcess {
  export const CancelOrders : Processes.ProcessAlgorithmSymbol = registerSymbol("ProcessAlgorithm", "OrderProcess.CancelOrders");
}

export module OrderQuery {
  export const OrderLines = new QueryKey("OrderQuery", "OrderLines");
}

export const OrderState = new EnumType<OrderState>("OrderState");
export type OrderState =
  "New" |
  "Ordered" |
  "Shipped" |
  "Canceled";

export module OrderTask {
  export const CancelOldOrdersWithProcess : Scheduler.SimpleTaskSymbol = registerSymbol("SimpleTask", "OrderTask.CancelOldOrdersWithProcess");
  export const CancelOldOrders : Scheduler.SimpleTaskSymbol = registerSymbol("SimpleTask", "OrderTask.CancelOldOrders");
}

export const PersonEntity = new Type<PersonEntity>("Person");
export interface PersonEntity extends CustomerEntity {
  Type: "Person";
  firstName: string;
  lastName: string;
  title: string | null;
  dateOfBirth: string | null;
  corrupt: boolean;
}

export const Player = new Type<Player>("Player");
export interface Player extends Entities.Entity {
  Type: "Player";
  name: string;
  characters: Entities.MList<Character>;
  joinedOn: string;
  attendance: number;
}

export module PlayerOperation {
  export const Save : Entities.ExecuteSymbol<Player> = registerSymbol("Operation", "PlayerOperation.Save");
}

export const ProductEntity = new Type<ProductEntity>("Product");
export interface ProductEntity extends Entities.Entity {
  Type: "Product";
  productName: string;
  supplier: Entities.Lite<SupplierEntity>;
  category: Entities.Lite<CategoryEntity>;
  quantityPerUnit: string;
  unitPrice: number;
  unitsInStock: number;
  reorderLevel: number;
  discontinued: boolean;
  additionalInformation: Entities.MList<AdditionalInformationEmbedded>;
}

export module ProductOperation {
  export const Save : Entities.ExecuteSymbol<ProductEntity> = registerSymbol("Operation", "ProductOperation.Save");
}

export module ProductQuery {
  export const CurrentProducts = new QueryKey("ProductQuery", "CurrentProducts");
}

export const Race = new EnumType<Race>("Race");
export type Race =
  "Tauren" |
  "Orc" |
  "Troll" |
  "Undead" |
  "Bloodelf";

export const Raid = new Type<Raid>("Raid");
export interface Raid extends Entities.Entity {
  Type: "Raid";
  name: string;
}

export module RaidOperation {
  export const Save : Entities.ExecuteSymbol<Raid> = registerSymbol("Operation", "RaidOperation.Save");
}

export const RegionEntity = new Type<RegionEntity>("Region");
export interface RegionEntity extends Entities.Entity {
  Type: "Region";
  description: string;
}

export module RegionOperation {
  export const Save : Entities.ExecuteSymbol<RegionEntity> = registerSymbol("Operation", "RegionOperation.Save");
}

export module RG2Group {
  export const UserEntities : Basics.TypeConditionSymbol = registerSymbol("TypeCondition", "RG2Group.UserEntities");
  export const RoleEntities : Basics.TypeConditionSymbol = registerSymbol("TypeCondition", "RG2Group.RoleEntities");
  export const CurrentCustomer : Basics.TypeConditionSymbol = registerSymbol("TypeCondition", "RG2Group.CurrentCustomer");
}

export const ShipperEntity = new Type<ShipperEntity>("Shipper");
export interface ShipperEntity extends Entities.Entity {
  Type: "Shipper";
  companyName: string;
  phone: string;
}

export module ShipperOperation {
  export const Save : Entities.ExecuteSymbol<ShipperEntity> = registerSymbol("Operation", "ShipperOperation.Save");
}

export const Spec = new Type<Spec>("Spec");
export interface Spec extends Entities.Entity {
  Type: "Spec";
  name: string;
  class: Entities.Lite<Class>;
}

export module SpecOperation {
  export const Save : Entities.ExecuteSymbol<Spec> = registerSymbol("Operation", "SpecOperation.Save");
}

export const SupplierEntity = new Type<SupplierEntity>("Supplier");
export interface SupplierEntity extends Entities.Entity {
  Type: "Supplier";
  companyName: string;
  contactName: string | null;
  contactTitle: string | null;
  address: AddressEmbedded;
  phone: string;
  fax: string;
  homePage: string | null;
}

export module SupplierOperation {
  export const Save : Entities.ExecuteSymbol<SupplierEntity> = registerSymbol("Operation", "SupplierOperation.Save");
}

export const TerritoryEntity = new Type<TerritoryEntity>("Territory");
export interface TerritoryEntity extends Entities.Entity {
  Type: "Territory";
  region: RegionEntity;
  description: string;
}

export module TerritoryOperation {
  export const Save : Entities.ExecuteSymbol<TerritoryEntity> = registerSymbol("Operation", "TerritoryOperation.Save");
}

export const TranslationConfigurationEmbedded = new Type<TranslationConfigurationEmbedded>("TranslationConfigurationEmbedded");
export interface TranslationConfigurationEmbedded extends Entities.EmbeddedEntity {
  Type: "TranslationConfigurationEmbedded";
  azureCognitiveServicesAPIKey: string | null;
  azureCognitiveServicesRegion: string | null;
  deepLAPIKey: string | null;
}

export const UserEmployeeMixin = new Type<UserEmployeeMixin>("UserEmployeeMixin");
export interface UserEmployeeMixin extends Entities.MixinEntity {
  Type: "UserEmployeeMixin";
  allowLogin: AllowLogin;
  employee: Entities.Lite<EmployeeEntity> | null;
}

export const WishItem = new Type<WishItem>("WishItem");
export interface WishItem extends Entities.Entity {
  Type: "WishItem";
  item: Entities.Lite<Item>;
  priorityNumber: number;
  ofPlayer: Entities.Lite<Player>;
}

export module WishItemOperation {
  export const Save : Entities.ExecuteSymbol<WishItem> = registerSymbol("Operation", "WishItemOperation.Save");
}


