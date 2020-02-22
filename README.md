## Run application

Go the project's directory and `dotnet run` from it:

```sh
~ $ cd ~/projects/belong/
~/projects/belong $ dotnet run
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /Users/fernando/projects/belong
```

Make a test request:

```sh
curl -ks https://localhost:5001/hosts/5 | jq
```

You should get something like the following:

```json
{
  "id": 5,
  "name": "Jon5 Host Dapper",
  "createdOn": "2020-02-18T05:27:03.673"
}
```