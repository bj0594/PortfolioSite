# Specification – PortfolioSite

## 1 The project in my own words

A portfolio site for employers and recruiters who want to see what kinds of projects I make.
The visitor can browse the projects I have chosen, open a project to read what I made and learned, and go to its live demo or GitHub repo.
Delivered as a Blazor WebAssembly site on GitHub Pages, built from a public GitHub repo.

Parts: Core, Web, Workflows, Docs, Mermaid

## 2 Requirements

Main flow: an employer opens the site, sees the projects I have chosen, opens one, and goes on to its live demo or its repo on GitHub.

Core (MUST):

- List the projects from projects.json in the order of the file (with title, short description, tech and image)
- Open a project page (with the full description, what I learned, tech and links)
- Link each project to its repo on GitHub
- Show a "Live demo" button when the project has a demo address

Rules:

- A project without an image shows a placeholder
- When projects.json cannot be read, the page shows "Could not load projects"
- When the list is empty, the page shows "No projects yet"
- When a project address does not exist, the page shows "Project not found"

Later / bonus (NOT NOW):

- Featured projects first, then the rest by date
- Hide a project without removing it from the file
- Fetch stars and last update from the GitHub API
- Filter by tech
- Live terminal for console applications

## 3 Flow

![Main flow](diagrams/main-flow.svg)

An employer opens the site, sees the projects from projects.json, opens one, and goes on to the live demo or the repo. 
Each diamond is a rule from §2. ([Mermaid source](diagrams/main-flow.mmd))

## 4 Interface

```text
Project: Slug (string), Title (string), Summary (string), Description (string),
         Learned (string), Tech (List<string>), Image (string?), RepoUrl (string), DemoUrl (string?)
Example: "book-keeper", "BookKeeper", "A library API with loans", "...", "TDD with xUnit",
         ["C#", "ASP.NET Core"], "images/book-keeper.png", "https://github.com/bj0594/BookKeeper", null
 
Project
- bool HasDemo                → true when DemoUrl is set and not blank
- string ImageOrPlaceholder   → Image, or "images/placeholder.png" when Image is missing or blank
 
ProjectService(List<Project> projects)
- List<Project> GetProjects()       → all projects, in the order of the file
- Project GetProject(string slug)   → the project with that slug
                                      unknown slug: KeyNotFoundException
```
 
Dependencies: none in Core. The Web project reads the projects from the data file and gives them to ProjectService.
 
### Web pages
 
```text
Hosting: Blazor WebAssembly on GitHub Pages
Client:  data file (see Data file)
Menu:    Projects
 
ProjectsPage        /                 shows:  one card per project (title, summary, tech, image or placeholder)
                                      does:   clicking a card opens the project
                                      calls:  GET data/projects.json
                                      states: loading, empty ("No projects yet"), error ("Could not load projects")
ProjectDetailsPage  /projects/{slug}  shows:  title, description, what I learned, tech, image or placeholder
                                      does:   "Repo on GitHub" link, "Live demo" button when HasDemo
                                      calls:  GET data/projects.json
                                      states: loading, not found ("Project not found"), error ("Could not load projects")
```
 
### Data file
 
```text
File:   PortfolioSite.Web/wwwroot/data/projects.json   (GET data/projects.json)
Fields: slug (string), title (string), summary (string), description (string), learned (string),
        tech (string[]), image (string, optional), repoUrl (string), demoUrl (string, optional)
Saved example: PortfolioSite.Web.Tests/Responses/projects.json
New project: add an object to the file, commit and push
```

## 5 Test list

### List the projects
- [x] Core: all projects are listed in the order of the file
- [x] Web: the page shows one card per project
- [x] Web client: GetProjectsAsync calls GET data/projects.json and reads the list

### Show a placeholder when a project has no image
- [ ] Core: ImageOrPlaceholder gives the placeholder when Image is missing or blank
- [ ] Web: the card shows the placeholder image

### Open a project page
- [x] Core: a project can be found by its slug
- [x] Web: clicking a card opens the project page with the description and what I learned

### Open a project that does not exist
- [x] Core: GetProject fails for an unknown slug
- [x] Web: the page shows "Project not found"

### Link to the repo and the demo
- [ ] Core: HasDemo is true when DemoUrl is set, and false when it is missing or blank
- [ ] Web: the project page links to the repo, and shows "Live demo" only when HasDemo

### Empty list and errors
- [ ] Web: the page shows "No projects yet" when the list is empty
- [ ] Web: the page shows "Could not load projects" when the file cannot be read

### Infrastructure
- [ ] Workflows: build and test run on every push and pull request
- [ ] Pages: the published page answers 200 with the right base href

Manual checks:
- [x] The workflow is green in Actions
- [ ] The site opens on GitHub Pages, also on a page address
- [ ] The site is readable on a phone

Later questions:
- [ ] Should two projects be allowed to have the same slug?

## 6 Questions, assumptions and decisions

- Decision: the projects are in a JSON file in the repo, and I add them by hand (I choose what is shown, and write my own text)
- Decision: Blazor WebAssembly on GitHub Pages (free, no server)
- Decision: no API, no database, no Docker and no login (the site only reads one file)
- Decision: a project page has a readable address, /projects/{slug}, instead of an id
- Decision: the projects are shown in the order of the file (featured first and sorting by date are in Later)
- Decision: a 404.html redirect on GitHub Pages, so /projects/{slug} also works when opened directly
- Decision: diagrams are Mermaid files in diagrams/, saved as SVG and shown as images
- Assumption: every project in the file has a public repo on GitHub
