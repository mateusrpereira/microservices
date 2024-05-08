# Sistema de Reservas de Hotel
É uma API que permite o registro e controle de quartos e hóspede em um Sistema de Reserva de Hotel, bem como o hóspede de realizar uma reserva e efetuar o pagamento por esta. 
A solução foi desenvolvida utilizando a linguagem C# na versão 6.0 do .NET Framework e Banco de Dados SQL Server.

## Requisitos:
Disponibilizar endpoint para inclusão de quartos (Room).

Disponibilizar endpoints para inclusão e busca de Hóspedes (Guest). A buscas por hóspedes devem ocorrer pelo seu código (Id).

Disponibilizar endpoint para inclusão de reserva de quartos (Booking).

Disponibilizar endpoint de pagamento de reservas (Payment).

CARACTERÍSTICAS:
- Gerenciar clientes
- Gerenciar quartos de hotel
- Permitir o cliente criar uma reserva

REGRAS DO NEGÓCIO:
- Não é possível reservar um quarto ocupado
- Aguarde 3 horas após o término da reserva para limpeza
- Alguns quartos podem estar indisponíveis para manutenção

STATUS DE RESERVAS
- Reserva criada
- Cancelado
- Pago
- Finalizado
- Reembolso

## Critérios de aceite:
Para cadastro de quartos deverá ser informado um nome, andar, se está em manutenção ou não, preço e a moeda para pagamento.

POST /Room

```
{
  "name": "Room 1",
  "level": 5,
  "inMaintenance": false,
  "price": 120,
  "currency": "Dollar"
}
```

Para registro do hóspede deverá informar nome, sobrenome, e-mail, número documento e tipo de documento. O documento deve ter pelo menos 4 caracteres e o documento aceito pode ser passaporte (cód. 1) ou carteira de motorista (cód. 2).

POST /Guest

```
{
  "name": "João",
  "surname": "Silva",
  "email": "joao.silva@email.com",
  "idNumber": "1234",
  "idTypeCode": 1
}
```

Para buscar pelo hóspede, deverá ser informado seu código (Id).

GET /Guest

```
{
  "guestId": "1",
}
```

Para realizar uma nova Reserva, deverá ser informado a data de entrada, data de saída, código do quarto e código do hóspede.

POST /Booking

```
{
  "placedAt": "2024-05-08T17:22:02.079Z",
  "start": "2024-05-08T17:22:02.079Z",
  "end": "2024-05-09T17:22:02.079Z",
  "roomId": 12,
  "guestId": 1
}
```

Para realizar o pagamento da reserva, deverá ser informado o meio de pagamento, o provedor de pagamento (PayPal, Stripe, PagSeguro, MercadoPago) e o método de pagamento (Cartão de Débito, Cartão de Crédito ou Transferência Bancária).

POST /Booking/{bookingId}/Pay

```
{
  "bookingId": 1,
  "paymentIntention": "https://mercado.com/asdf",
  "selectedPaymentProvider": "MercadoPago",
  "selectedPaymentMethod": "DebitCard"
}
```

## Execução:
Abra a solução (HotelBooking.sln), preferencialmente, na versão 2022 ou posterior do Microsoft Visual Studio.

Ter instalado o SQL Server Management Studio, de preferência a versão 19 ou posterior.

Altere a string de conexão (ConnectionString) da base de dados :
```
Path:
HotelBooking\BookingService\Consumers\API\appsettings.json

  "ConnectionStrings": {
    "Main": "Data Source=(localdb)\\MSSQLLocalDB;Database=HotelManagement;Integrated Security=true"
  },
```

## Banco de dados:
Utilizar o Entity Framework (pasta Migrations no projeto BookingService):
```
BookingService\Adapters\Data
```

Abra o Console do Gerenciador de Pacotes e gere a Migration:

```
Add-Migration MigrationHotelBooking
Update-Database
```

## Arquitetura Hexagonal, DDD, TDD

-- Tabelas e recursos do banco de dados da aplicação:

![image](https://hackmd.io/_uploads/B16lo4KfA.png)

-- Visão geral e dependências dos componentes das camadas de serviço:

![image](https://hackmd.io/_uploads/Sy1GiEFGA.png)

-- Visão geral dos serviços:

![image](https://hackmd.io/_uploads/HyYMTEFMR.png)


-- Troca de informações entre as camadas:

![image](https://hackmd.io/_uploads/SJBNoNtMA.png)

-- Interação entre as camadas e serviços:

![image](https://hackmd.io/_uploads/SJmBo4FG0.png)


![image](https://hackmd.io/_uploads/SkW36NFzC.png)






