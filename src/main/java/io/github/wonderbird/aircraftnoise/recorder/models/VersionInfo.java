package io.github.wonderbird.aircraftnoise.recorder.models;

public class VersionInfo {
    private String version;

    public VersionInfo() {}
    public VersionInfo(String version) {
        this.version = version;
    }

    public String getVersion() {
        return version;
    }
    public void setVersion(String version) { this.version = version; }
}
