package io.github.wonderbird.aircraftnoise.recorder;

import java.util.HashSet;
import java.util.Set;

public class DirectoryList {
    private Set<String> files = new HashSet<>();

    public Set<String> getFiles() {
        return files;
    }

    public void addFile(String filename) {
        files.add(filename);
    }
}
