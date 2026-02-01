# Poderosa

## Getting Started

### Git Authentication Setup

To work with this repository, you need to configure Git authentication properly. GitHub no longer supports password authentication for Git operations. You have two options:

#### Option 1: Using SSH (Recommended)

1. Generate an SSH key (if you don't have one):
   ```bash
   ssh-keygen -t ed25519 -C "your_email@example.com"
   ```

2. Add your SSH key to the ssh-agent:
   ```bash
   eval "$(ssh-agent -s)"
   ssh-add ~/.ssh/id_ed25519
   ```

3. Add your SSH public key to your GitHub account:
   - Copy your public key: `cat ~/.ssh/id_ed25519.pub`
   - Go to GitHub Settings → SSH and GPG keys → New SSH key
   - Paste your key and save

4. Update your repository remote to use SSH:
   ```bash
   git remote set-url origin git@github.com:web-salta/poderosa.git
   ```

#### Option 2: Using Personal Access Token (PAT)

1. Create a Personal Access Token on GitHub:
   - Go to GitHub Settings → Developer settings → Personal access tokens → Tokens (classic)
   - Click "Generate new token (classic)"
   - Select scopes: `repo` (full control of private repositories)
   - Generate and copy the token

2. Configure Git to use the token:
   ```bash
   # Use the token as password when prompted
   git push origin main
   # Username: your-github-username
   # Password: your-personal-access-token
   ```

3. To avoid entering credentials repeatedly, use Git credential helper:
   ```bash
   # For macOS:
   git config --global credential.helper osxkeychain
   
   # For Windows:
   git config --global credential.helper wincred
   
   # For Linux:
   git config --global credential.helper store
   ```

### Cloning the Repository

```bash
# Using SSH (recommended)
git clone git@github.com:web-salta/poderosa.git

# Using HTTPS (requires PAT)
git clone https://github.com/web-salta/poderosa.git
```

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed contribution guidelines.
