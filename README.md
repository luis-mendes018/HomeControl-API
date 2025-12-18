<h1>Home Control - README</h1>

<h2>1. Requisitos</h2>
<ul>
    <li>.NET 9 SDK</li>
    <li>MySQL</li>
    <li>(Opcional) MySQL Workbench para visualizar o banco</li>
</ul>

<h2>2. Configuração do banco</h2>
<p>Configure a string de conexão no <code>appsettings.json</code>:</p>
<pre><code>{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=home_control;User=root;Password=sua_senha;"
  }
}</code></pre>
<p>Ajuste <code>User</code> e <code>Password</code> conforme seu MySQL local.</p>

<h2>3. Aplicar migrations</h2>
<p>Execute o comando abaixo na pasta do projeto para criar o banco e as tabelas:</p>
<pre><code>dotnet ef database update</code></pre>
ou caso esteja usando a IDE do visual studio
<pre><code>update-database</code></pre>
<p>O EF Core criará automaticamente o banco <code>home_control</code> se ele não existir.</p>

<h2>4. Persistência de dados</h2>
<ul>
    <li>Não há dados iniciais obrigatórios.</li>
    <li>Os dados serão criados quando o usuário utilizar o sistema.</li>
    <li>Todos os dados persistem no banco mesmo após reiniciar a aplicação.</li>
</ul>

<h2>5. Executar a aplicação</h2>
<pre><code>dotnet run</code></pre>
ou o botão de executar na IDE do visual studio
<p>A aplicação estará disponível em <code>(https://localhost:7212)</code></p>

<h2>6. Observações</h2>
<ul>
    <li>As migrations cuidam da criação do schema do banco.</li>
    <li>Não é necessário inserir dados manualmente para testes.</li>
</ul>
